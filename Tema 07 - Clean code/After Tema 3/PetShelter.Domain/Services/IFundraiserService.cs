using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace PetShelter.Domain.Services;

public interface IFundraiserService
{
    Task DeleteFundraiserAsync(int fundraiserId);

    Task<Fundraiser?> GetFundraiser(int fundraiserId);

    Task<IReadOnlyCollection<Fundraiser>> GetAllFundraisers();

    Task<int> CreateFundraiser(Person owner, Fundraiser fundraiser);

    Task DonateToFundraiser(int fundraiserId, Donation donation);
}