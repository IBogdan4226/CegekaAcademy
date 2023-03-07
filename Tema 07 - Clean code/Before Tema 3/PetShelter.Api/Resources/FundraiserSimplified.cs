namespace PetShelter.Api.Resources
{
    public class FundraiserSimplified
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Target { get; set; }
        public decimal CurrentlyRaised { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public FundraiserStatus Status { get; set; }
    }
}
