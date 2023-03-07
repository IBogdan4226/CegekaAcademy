using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public class DonationRepository : BaseRepository<Donation>, IDonationRepository
{
    public DonationRepository(PetShelterContext context): base(context)
    {
    }

    public async Task<ICollection<Person>> GetAllDonors()
    {
        return await _context.Donations
            .Select(d => d.Donor)
            .Distinct()
            .ToListAsync();
    }

    public async Task<decimal> GetDonationRaisedMoney()
    {
        return await _context.Donations
            .Where(d => d.FundraiserId == null)
            .SumAsync(d => d.Amount);
    }
}