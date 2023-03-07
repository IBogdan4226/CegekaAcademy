using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public class FundraiserRepository:BaseRepository<Fundraiser>, IFundraiserRepository
{
    public FundraiserRepository(PetShelterContext context) : base(context){}

    public async Task<decimal> GetFundraiserMoneyById(int id)
    {
        decimal totalDonation = await _context.Fundraisers.Where(f => f.Id == id).SelectMany(f => f.Donations).SumAsync(d => d.Amount);
        return totalDonation;
    }

}