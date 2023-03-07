using PetShelter.Domain;
using PetShelter.Domain.Services;

namespace PetShelter.Api.Resources.Extensions;

public static class FundraiserExtension
{


    public static Domain.Fundraiser AsDomainModel(this AddedFundraiser fundraiser)
    {

        var domainModel = new Domain.Fundraiser
        {
            Owner = fundraiser.Owner.AsDomainModel(),
            Name = fundraiser.Name,
            DueDate = fundraiser.DueDate,
            Target = fundraiser.Target
        };
        return domainModel;
    }

    public static Resources.Fundraiser AsResource(this Domain.Fundraiser fundraiser)
    {
        var resourceModel = new Resources.Fundraiser
        {
            Id = fundraiser.Id,
            Name = fundraiser.Name,
            CreationDate = fundraiser.CreationDate,
            DueDate = fundraiser.DueDate,
            Target = fundraiser.Target,
            CurrentlyRaised = fundraiser.CurrentlyRaised,
            Status = fundraiser.Status
        };

        return resourceModel;
    }

    public static Resources.FundraiserDetailed AsDetailedResource(this Domain.Fundraiser fundraiser)
    {
        var resourceModel = new FundraiserDetailed
        {
            Id = fundraiser.Id,
            Name = fundraiser.Name,
            CreationDate = fundraiser.CreationDate,
            DueDate = fundraiser.DueDate,
            CurrentlyRaised = fundraiser.CurrentlyRaised,
            Target = fundraiser.Target,
            Status = fundraiser.Status,
            Owner= fundraiser.Owner.AsResource(),
            Donors = fundraiser.Donations.Select(p => p.Donor.AsResource()).ToList()
        };
        return resourceModel;
    }

}

    
