using FluentAssertions;
using Moq;
using PetShelter.BusinessLayer.BuilderDesignPattern;
using PetShelter.BusinessLayer.ExternalServices;
using PetShelter.BusinessLayer.Models;
using PetShelter.BusinessLayer.Validators;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;

namespace PetShelter.BusinessLayer.Tests
{
    public class RescuePetTests
    {
        private readonly IPersonService _personService;
        private readonly PetService _petServiceSut;

        private readonly Mock<IPersonRepository> _mockPersonRepository;
        private readonly Mock<IPetRepository> _mockPetRepository;
        private readonly Mock<IIdNumberValidator> _mockIdNumberValidator;

        private RescuePetRequest _request;

        public RescuePetTests()
        {
            _mockPersonRepository= new Mock<IPersonRepository>();
            _mockPetRepository= new Mock<IPetRepository>(); 
            _mockIdNumberValidator = new Mock<IIdNumberValidator>();

            _personService = new PersonService(_mockPersonRepository.Object, _mockIdNumberValidator.Object, new PersonValidator());
            _petServiceSut = new PetService(_personService, _mockPetRepository.Object, new RescuePetRequestValidator(), new AdoptPetRequestValidator());
        }

        private void SetupHappyPath()
        {
            _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(true);
            var builder = new RescuePetBuilder();
            var director = new RescuePetDirector(builder);

            _request = director.ConstructCuteCat();
        }

        [Fact]
        public async void GivenValidData_WhenRescuePet_PetIsAdded()
        {
            //Arrange
            SetupHappyPath();

            //Act
            await _petServiceSut.RescuePet(_request);

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Once);
        }

        [Theory]
        [InlineData(200)]
        [InlineData(-5)]
        [InlineData(0)]
        public async Task GiventWeightIsInvalid_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded(decimal weight)
        {
            // Arrange
            SetupHappyPath();
            _request.WeightInKg = weight;

            //Act
            await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }


        [Fact]
        public async Task GivenIdNumberIsInvalid_WhenRescuePet_ThenThowsArgumentException()
        {
            //Arrange
            SetupHappyPath();

            _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(false);

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            exception.Message.Should().Be("CNP format is invalid");

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }


        #region Tema 04. Testing
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task GivenPetNameIsInvalid_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded(string name)
        {
            // Arrange
            SetupHappyPath();
            _request.PetName = name;

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(null)]
        public async Task GivenTypeIsInvalid_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded(int type)
        {
            // Arrange
            SetupHappyPath();
            _request.Type = (Constants.PetType)type;

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }

        [Fact]
        
        public async Task GivenPersonNull_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded()
        {
            // Arrange
            SetupHappyPath();
            _request.Person = null;

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }

        [Fact]
        public async Task GivenPersonWithInvalidIdNumberLength_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded()
        {
            // Arrange
            SetupHappyPath();
            _request.Person.IdNumber="1";

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("B")]
        public async Task GivenPersonWithInvalidName_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded(string personName)
        {
            // Arrange
            SetupHappyPath();
            _request.Person.Name =  personName;

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }
        #endregion
    }
}