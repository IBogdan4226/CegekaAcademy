using Azure.Core;
using FluentValidation;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;

namespace PetShelter.BusinessLayer.Tests;

public class DonationService:IDonationService
{
    private readonly IDonationRepository _donationRepository;
    private readonly IPersonService _personService;
    private readonly IValidator<AddDonationRequest> _donationValidator;

    public DonationService(IDonationRepository donationRepository, IPersonService personService,  IValidator<AddDonationRequest> validator)
    {
        _donationValidator = validator;
        _personService = personService;
        _donationRepository = donationRepository;
    }

    public async Task AddDonation(AddDonationRequest addDonationRequest)
    {
        var validationResult = _donationValidator.Validate(addDonationRequest);
        if(!validationResult.IsValid) { throw new ArgumentException(); }

        var donor = await _personService.GetPerson(addDonationRequest.Donor);
        await _donationRepository.Add(new DataAccessLayer.Models.Donation
        {
            Amount= addDonationRequest.Amount,
            Donor = donor,
            DonorId=addDonationRequest.DonorId,
            
        });
    }
    public async Task<Donation> GetDonation(int id)
    {
        return await _donationRepository.GetById(id);
    }

    public async Task<IReadOnlyCollection<Donation>> GetAllDonations()
    {
        return await _donationRepository.GetAll();
    }

    public async Task UpdateDonation(int donationId, UpdateDonationRequest updateDonationRequest)
    {
        var donation = await _donationRepository.GetById(donationId);
        if (donation == null) { throw new ArgumentException(); }

        donation.Amount = updateDonationRequest.Amount;

        await _donationRepository.Update(donation);
    }
}