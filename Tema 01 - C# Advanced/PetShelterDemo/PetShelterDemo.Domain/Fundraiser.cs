namespace PetShelterDemo.Domain
{
    public class Fundraiser : INamedEntity
    {
        public string Name { get; }
        public string Description { get;}
        public double Target { get; }
        public Donation MoneyRaised { get; set;}
        private List<Person> DonorsList { get;}

        public Fundraiser(string name, string description, double target)
        {
            Name = name;
            Description = description;
            Target = target;
            MoneyRaised = new Donation() ;
            DonorsList = new List<Person>();
        }

        public void donateMoney(Person person,double amount,string currency)
        {
            if (amount > 0 &&person!=null)
            {
                MoneyRaised.AddDonation(currency, amount);
                DonorsList.Add(person);
            }
            else
            {
                Console.WriteLine("Invalid value!");
            }
        }
        public List<Person> getDonors()
        {
            return DonorsList;
        }
    }
}
