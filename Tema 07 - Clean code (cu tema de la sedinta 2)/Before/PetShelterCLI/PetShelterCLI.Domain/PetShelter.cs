using PetShelter.DataAccessLayer;
using PetShelter.DataAccessLayer.Repository;
using PetShelter.DataAccessLayer.Models;
namespace PetShelterCLI.PetShelterCLI.Domain;

public class PetShelter
{
    private readonly PetShelterContext database;
    private readonly IPetRepository petRepository;
    private readonly IPersonRepository personRepository;
    private readonly IDonationRepository donationRepository;
    private readonly IFundraiserRepository fundraiserRepository;
    public PetShelter()
    {
        database= new PetShelterContext();
        petRepository = new PetRepository(database);
        personRepository = new PersonRepository(database);
        donationRepository=new DonationRepository(database);
        fundraiserRepository = new FundraiserRepository(database);
    }

    public void RegisterPet(Pet pet)
    {
        petRepository.Add(pet);
    }

    public IReadOnlyList<Pet> GetAllPets()
    {
        return petRepository.GetAll().Result;
    }

    public Pet GetByName(string name)
    {
        return petRepository.GetPetByName(name).Result;
    }

    public async void Donate(decimal amount, Person person)
    {
        await personRepository.Add(person);
        await donationRepository.Add(new Donation(amount, person.Id));
    }

    public async void Donate(decimal amount, Person person,int fundraiserId)
    {
        await personRepository.Add(person);
        await donationRepository.Add(new Donation(amount, person.Id,fundraiserId));
    }
    public decimal GetTotalDonations()
    {
        return donationRepository.GetDonationRaisedMoney().Result;
    }

    public decimal GetCurrentDonationFromFundraiser(int id)
    {
        return  fundraiserRepository.GetFundraiserMoneyById(id).Result;
    }

    public IReadOnlyList<Person> GetAllDonors()
    {
        return (IReadOnlyList<Person>)donationRepository.GetAllDonors().Result;
    }

    public void RegisterFundraiser(Fundraiser fundraiser)
    {
        fundraiserRepository.Add(fundraiser);
    }
    public  IReadOnlyList<Fundraiser> GetAllFundraisers()
    {
        return fundraiserRepository.GetAll().Result;
    }
    public Fundraiser GetFundraiserById(int id)
    {
        return fundraiserRepository.GetById(id).Result;
    }
}