namespace PetShelter.DataAccessLayer.Models;

public class Fundraiser : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal CurrentlyRaised { get; set; }
    public decimal Target { get; set; }
    public int OwnerId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; }


    public ICollection<Donation> Donations { get; set; }
    public Person Owner { get; set; }
    public Fundraiser(string name, decimal target,int ownerId, DateTime dueDate)
    {
        Name = name;
        Target = target;
        OwnerId = ownerId;
        CreationDate = DateTime.Now;
        DueDate = dueDate;
        Status = "Active";
        CurrentlyRaised = 0;
        Donations= new List<Donation>();
    }
}