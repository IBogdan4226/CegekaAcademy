namespace PetShelterDemo.Domain
{
    public class Fundraiser : INamedEntity
    {
        public string Name { get; }
        public string Description { get;}
        public double Target { get; }
        public double MoneyRaised { get; set;}
        private List<Person> DonorsList { get;}

        public Fundraiser(string name, string description, double target)
        {
            Name = name;
            Description = description;
            Target = target;
            MoneyRaised = 0;
            DonorsList = new List<Person>();
        }

        public void donateMoney(Person person,double amount)
        {
            if (amount > 0 &&person!=null)
            {
                MoneyRaised += amount;
                DonorsList.Add(person);
            }
            else
            {
                Console.WriteLine("Nup!");
            }
        }
        public List<Person> getDonors()
        {
            return DonorsList;
        }
    }
}
