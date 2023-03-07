using PetShelter.Domain;

namespace PetShelter.Api.Resources;

public class AddedFundraiser 
{
    public string Name { get; set; }
    public decimal Target { get; set; }
    public DateTime DueDate { get; set; }
    public Person Owner { get; set; }
}
