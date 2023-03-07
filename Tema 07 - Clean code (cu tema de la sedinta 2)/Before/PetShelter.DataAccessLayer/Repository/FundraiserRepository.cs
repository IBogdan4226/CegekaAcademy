using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public class FundraiserRepository:BaseRepository<Fundraiser>, IFundraiserRepository
{
    public FundraiserRepository(PetShelterContext context) : base(context){}

    public async Task<decimal> GetFundraiserMoneyById(int id)
    {
        var fundraiser = await _context.Fundraisers.FirstOrDefaultAsync(p => p.Id == id);
        if (fundraiser == null)
        {
            throw new ArgumentException($"Fundraiser with id {id} not found.");
        }

        decimal totalDonation = fundraiser.Donations?.Sum(d => d.Amount) ?? 0;
        return totalDonation;
    }

}