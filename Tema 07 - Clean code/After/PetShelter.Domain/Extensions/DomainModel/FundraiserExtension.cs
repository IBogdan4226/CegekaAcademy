using PetShelter.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.Domain.Extensions.DomainModel
{
    internal static class FundraiserExtension
    {
        public static Fundraiser? toDomainModel(this DataAccessLayer.Models.Fundraiser fundraiser)
        {
            if (fundraiser == null)
            {
                return null;
            }
            var fundraiserType=Enum.Parse<FundraiserStatus>(fundraiser.Status);
            var domainModel = new Fundraiser();
            domainModel.Id = fundraiser.Id;
            domainModel.Name = fundraiser.Name;
            domainModel.DueDate= fundraiser.DueDate;
            domainModel.CreationDate= fundraiser.CreationDate;
            domainModel.CurrentlyRaised= fundraiser.CurrentlyRaised;
            domainModel.Target= fundraiser.Target;
            domainModel.Owner = fundraiser.Owner.ToDomainModel();
            domainModel.Status = fundraiserType;
            domainModel.Donations= fundraiser.Donations.Select(p=>p.toDomainModel()).ToList();
            return domainModel;
        }
    }
}
