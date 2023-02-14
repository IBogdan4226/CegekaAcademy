using PetShelterDemo.DAL;

namespace PetShelterDemo.Domain;

public class PetShelter
{
    private readonly IRegistry<Pet> petRegistry;
    private readonly IRegistry<Person> donorRegistry;
    private readonly IRegistry<Fundraiser> fundraiserRegistry;
    private Donation donations;

    public PetShelter()
    {
        donorRegistry = new Registry<Person>(new Database());
        petRegistry = new Registry<Pet>(new Database());
        fundraiserRegistry= new Registry<Fundraiser>(new Database());
        donations = new Donation();
    }

    public void RegisterPet(Pet pet)
    {
        petRegistry.Register(pet);
    }

    public IReadOnlyList<Pet> GetAllPets()
    {
        return petRegistry.GetAll().Result; // Actually blocks thread until the result is available.
    }

    public Pet GetByName(string name)
    {
        return petRegistry.GetByName(name).Result;
    }

    public void Donate(Person donor)
    {
        donorRegistry.Register(donor);
        var currency = donations.GetCurrencyChoice();
        var amount = donations.GetDonationAmount();
        donations.AddDonation(currency, amount);
    }

    
    public int GetTotalDonationsInRON()
    {
        return (int) donations.CalculateValue();
    }

    public IReadOnlyList<Person> GetAllDonors()
    {
        return donorRegistry.GetAll().Result;
    }

    public void RegisterFundraiser(Fundraiser fundraiser)
    {
        fundraiserRegistry.Register(fundraiser);
    }
    public void Donate(Person doner, double amount, Fundraiser fundraiser)
    {
        fundraiser.donateMoney(doner, amount);
    }
    public  IReadOnlyList<Fundraiser> GetAllFundraisers()
    {
        return fundraiserRegistry.GetAll().Result;
    }
    public Fundraiser GetByNameFundraiser(string name)
    {
        return fundraiserRegistry.GetByName(name).Result;
    }
}