using PetShelter.Domain;
using PetShelter.Domain.Services;

namespace PetShelter.Api.Resources.Extensions;

public static class DonateExtension
{


    public static Domain.Donation AsDomainModel(this DonateRequiest donation)
    {
        var domainModel = new Domain.Donation();
        domainModel.Amount = donation.Amount;
        domainModel.Donor = donation.Donor.AsDomainModel();
        return domainModel;
    }


}
