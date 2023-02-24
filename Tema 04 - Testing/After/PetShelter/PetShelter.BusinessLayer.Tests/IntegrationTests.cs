using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;
using PetShelter.DataAccessLayer;
using FluentAssertions;
using PetShelter.BusinessLayer.ExternalServices;
using System.Security.Cryptography;

namespace PetShelter.BusinessLayer.Tests
{
    public class IntegrationTests
    {
        private readonly PetShelterContext _petShelterContext;
        private IPetRepository _petRepositorySut;
        private IDonationRepository _donationRepository;
        private IIdNumberValidator _idNumberValidator;
        public IntegrationTests()
        {
            _petShelterContext = new PetShelterContext();
            _petRepositorySut = new PetRepository(_petShelterContext);
            _donationRepository= new DonationRepository(_petShelterContext);
            _idNumberValidator = new IdNumberValidator(new HttpClient());
        }

      

        [Fact]
        public async void GivenSavingAnPet_WhenGetPetByName_ReturnsPetInserted()
        {
            //Arrange 
            const string? petName = "Bobita";
            var _newPet = new Pet {
                Name = petName,
                Birthdate = DateTime.Now,
                Description = "Bobita e catelul meu frumos!<3",
                ImageUrl = "bobita.jpg",
                IsHealthy = true,
                IsSheltered = true,
                WeightInKg = 6,
                Type = "Dog"
            };

            //Act
            await _petRepositorySut.Add(_newPet);
            var pet = _petRepositorySut.GetPetByName(petName).Result;
           
            //Assert
            pet.Should().NotBeNull();
            pet.Name.Should().Be(petName);


            Dispose(_newPet);
        }

        [Fact]
        public async void GivenSavingAnPet_WhenUpdatingThePet_ReturnsPetUpdated()
        {
            //Arrange 
            const string? petName = "Bobita";
            const string? newName = "Bobita1";
            var _newPet = new Pet
            {
                Name = petName,
                Birthdate = DateTime.Now,
                Description = "Bobita e catelul meu frumos!<3",
                ImageUrl = "bobita.jpg",
                IsHealthy = true,
                IsSheltered = true,
                WeightInKg = 6,
                Type = "Dog"
            };

           
            //Act
            await _petRepositorySut.Add(_newPet);

            var updatedPet = _newPet;
            updatedPet.Name = newName;

            await _petRepositorySut.Update(updatedPet);

            var pet = _petRepositorySut.GetPetByName(newName).Result;
            //Assert
            pet.Should().NotBeNull();
            pet.Name.Should().Be(newName);


            Dispose<Pet>(updatedPet);
        }


        [Fact]
        public async void GivenSavingADonation_WhenGetDonationById_ReturnsDonationInserted()
        {
            //Arrange 
            
            var newDonation = new Donation
            {
                Amount = 1,
                DonorId = 1,
                Donor = new Person
                {
                    DateOfBirth = DateTime.Now.AddYears(-Constants.PersonConstants.AdultMinAge),
                    IdNumber = "1111222233334",
                    Name = "TestName"
                }
            };

            //Act
            await _donationRepository.Add(newDonation);
            var donation = _donationRepository.GetById(newDonation.Id).Result;

            //Assert
            donation.Should().NotBeNull();
            donation.Amount.Should().Be(newDonation.Amount);


            Dispose(donation);
        }

        [Fact]
        public async void GivenValidCNP_WhenValidate_ReturnsTrue()
        {
            //Arrange 
            const string cnp = "1111222233334";
          
            //Act
            var isOK= await _idNumberValidator.Validate(cnp);
            //Assert
            isOK.Should().BeTrue();

        }

        [Fact]
        public async void GivenInvalidCNP_WhenValidate_ReturnsFalse()
        {
            //Arrange 
            const string cnp = "11";

            //Act
            var isOK = await _idNumberValidator.Validate(cnp);
            //Assert
            isOK.Should().BeFalse();

        }
        public void  Dispose<T>(T inserted)
        {
            _petShelterContext.Remove(inserted);
            _petShelterContext.SaveChanges();
        }
    }
}
