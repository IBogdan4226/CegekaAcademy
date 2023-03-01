using PetShelter.Domain;
using PetShelter.Domain.Services;

namespace PetShelter.Api.Resources.Extensions;

public static class FundraiserExtension
{
    

    public static Domain.Fundraiser AsDomainModel(this AddedFundraiser fundraiser)
    {

        var domainModel = new Domain.Fundraiser();
        domainModel.Owner = fundraiser.Owner.AsDomainModel();
        domainModel.Name = fundraiser.Name;
        domainModel.DueDate = fundraiser.DueDate;
        domainModel.Target = fundraiser.Target;
        return domainModel;
    }


}
