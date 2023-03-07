using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public class FundraiserRepository : BaseRepository<Fundraiser>, IFundraiserRepository
{
    public FundraiserRepository(PetShelterContext context) : base(context) { }

    public async Task<Fundraiser?> DonateToFundraiser(int fundraiserId, decimal amount)
    {
        var fundraiser = await this.GetById(fundraiserId);
        if (fundraiser == null)
        {
            return null;
        }
        fundraiser.CurrentlyRaised += amount;
        if (fundraiser.CurrentlyRaised > fundraiser.Target)
        {
            fundraiser.Status = FundraiserStatus.Closed.ToString();
        }
        await this.Update(fundraiser);
        return fundraiser;

    }
}
