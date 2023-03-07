namespace PetShelter.DataAccessLayer.Models;

public class Donation: IEntity
{
    public int Id { get; set; }
    public decimal Amount { get; set; }

    /// <summary>
    ///     FK to a person
    /// </summary>
    public int DonorId { get; set; }
    public int? FundraiserId { get; set; }
    public Person Donor { get; set; }

    public Fundraiser? Fundraiser { get; set; }

    public Donation(decimal amount, int donorId, int? fundraiserId = null)
    {
        Amount = amount;
        DonorId = donorId;
        FundraiserId = fundraiserId;
    }
}