namespace PetShelter.DataAccessLayer.Models;

public class Fundraiser:IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Target { get; set; }

    public ICollection<Donation> Donations { get; set; }

    public Fundraiser(string name, decimal target)
    {
        Name = name;
        Target = target;
    }
}