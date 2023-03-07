using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public interface IFundraiserRepository : IBaseRepository<Fundraiser>
{
    Task<Fundraiser?> DonateToFundraiser(int fundraiserId,decimal amount);
}
