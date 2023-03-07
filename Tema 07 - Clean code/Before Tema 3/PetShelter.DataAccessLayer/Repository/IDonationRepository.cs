using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public interface IDonationRepository: IBaseRepository<Donation>
{
    public Task<ICollection<Donation>> GetAllDonorsFromFundraiser(int fundraiserId);
}