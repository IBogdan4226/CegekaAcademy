using PetShelter.Domain.Services;

namespace PetShelter.Api.Resources
{
    public class FundraiserDetailed:Fundraiser
    {
        public Person Owner { get; set; }
        public ICollection<Person> Donors { get; set; }
    }
}
