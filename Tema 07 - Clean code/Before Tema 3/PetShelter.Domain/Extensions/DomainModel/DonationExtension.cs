using PetShelter.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.Domain.Extensions.DomainModel
{
    internal static class DonationExtension
    {
        public static Donation? toDomainModel(this DataAccessLayer.Models.Donation donation)
        {
            if (donation == null)
            {
                return null;
            }
            var domainModel = new Donation();
            domainModel.DonorId = donation.DonorId;
            domainModel.Donor = donation.Donor.ToDomainModel();
            domainModel.Amount= donation.Amount;
            return domainModel;

        }
    }
}
