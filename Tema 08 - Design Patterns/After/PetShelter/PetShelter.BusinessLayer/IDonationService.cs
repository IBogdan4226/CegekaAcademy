using PetShelter.DataAccessLayer.Models;

namespace PetShelter.BusinessLayer.Tests
{
    public interface IDonationService
    {
        Task<IReadOnlyCollection<Donation>> GetAllDonations();
        Task AddDonation(AddDonationRequest addDonationRequest);
        Task<Donation> GetDonation(int id);
        Task UpdateDonation(int donationId, UpdateDonationRequest updateDonationRequest);
    
    }
}