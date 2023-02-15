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
        var currency = Donation.GetCurrencyChoice();
        var amount = Donation.GetDonationAmount();
        donations.AddDonation(currency, amount);
    }

    #region HOMEWORK Tema 01 - C# Advanced
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
    public void Donate(Person doner, string currency, double amount, Fundraiser fundraiser)
    {
        
        fundraiser.donateMoney(doner, amount,currency);
    }
    public  IReadOnlyList<Fundraiser> GetAllFundraisers()
    {
        return fundraiserRegistry.GetAll().Result;
    }
    public Fundraiser GetByNameFundraiser(string name)
    {
        return fundraiserRegistry.GetByName(name).Result;
    }
    #endregion
}