using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public interface IDonationRepository: IBaseRepository<Donation>
{
    Task<decimal> GetDonationRaisedMoney();
    Task<ICollection<Person>> GetAllDonors();
}