using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public interface IFundraiserRepository : IBaseRepository<Fundraiser>
{
    Task<decimal> GetFundraiserMoneyById(int id);
}