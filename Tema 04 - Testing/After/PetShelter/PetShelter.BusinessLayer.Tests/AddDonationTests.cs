using Moq;
using PetShelter.BusinessLayer.ExternalServices;
using PetShelter.BusinessLayer.Models;
using PetShelter.BusinessLayer.Validators;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;

namespace PetShelter.BusinessLayer.Tests;

public class AddDonationTests
{
    private readonly Mock<IDonationRepository> _mockDonationRepository;
    private readonly DonationService _donationServiceSut;

    private readonly IPersonService _personService;
    private readonly Mock<IPersonRepository> _mockPersonRepository;
    private readonly Mock<IIdNumberValidator> _mockIdNumberValidator;

    private AddDonationRequest _request;
    public AddDonationTests()
    {
        _mockPersonRepository = new Mock<IPersonRepository>();
        _mockIdNumberValidator = new Mock<IIdNumberValidator>();
        _personService = new PersonService(_mockPersonRepository.Object, _mockIdNumberValidator.Object, new PersonValidator());

        _mockDonationRepository = new Mock<IDonationRepository>();
        _donationServiceSut= new DonationService(_mockDonationRepository.Object,_personService, new AddDonationRequestValidator());
    }


    private void SetupHappyPath()
    {
        _request = new AddDonationRequest
        {
            Amount=1,
            DonorId=1,
            Donor = new BusinessLayer.Models.Person
            {
                DateOfBirth = DateTime.Now.AddYears(-Constants.PersonConstants.AdultMinAge),
                IdNumber = "1111222233334",
                Name = "TestName"
            }
        };
    }
    [Fact]
    public async Task GivenValidRequest_WhenAddDonation_DonationIsAdded()
    {
        //Arrange 
        _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(true);
        SetupHappyPath();
       
        //Act
        await _donationServiceSut.AddDonation(_request);

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.Is<Donation>(d => d.Amount == _request.Amount)), Times.Once);
    }

    [Fact]
    public async Task GivenRequestWithMissingAmount_WhenAddDonation_DonationIsNotAdded()
    {
        //Arrange 
        SetupHappyPath();
        _request.Amount = default(decimal);

        //Act
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    #region Tema 04. Testing
    [Theory]
    [InlineData(-1)]
    [InlineData(-0.0001)]
    public async Task GivenRequestWithAmountNegative_WhenAddDonation_DonationIsNotAdded(decimal amount)
    {
        //Arrange 
        SetupHappyPath();
        _request.Amount = amount;

        //Act
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Fact]
    public async Task GivenRequestWithMissingPerson_WhenAddDonation_DonationIsNotAdded()
    {
        //Arrange 
        SetupHappyPath();
        _request.Donor = null;

        //Act
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("B")]
    public async Task GivenRequestWithInvalidDonorName_WhenAddDonation_DonationIsNotAdded(string donorName)
    {
        //Arrange 
        SetupHappyPath();
        _request.Donor.Name = donorName;

        //Act
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(-1)]
    public async Task GivenRequestWithInvalidDonorId_WhenAddDonation_DonationIsNotAdded(int donorId)
    {
        //Arrange 
        SetupHappyPath();
        _request.DonorId = donorId;

        //Act
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Fact]
    public async Task GivenRequestWithDonorMinor_WhenAddDonation_DonationIsNotAdded()
    {
        //Arrange 
        SetupHappyPath();
        _request.Donor.DateOfBirth= DateTime.Today;

        //Act
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }


    #endregion
}
