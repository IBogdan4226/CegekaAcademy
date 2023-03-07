//// See https://aka.ms/new-console-template for more information
//// Syntactic sugar: Starting with .Net 6, Program.cs only contains the code that is in the Main method.
//// This means we no longer need to write the following code, but the compiler still creates the Program class with the Main method:
//// namespace PetShelterDemo
//// {
////    internal class Program
////    {
////        static void Main(string[] args)
////        { actual code here }
////    }
//// }


using PetShelter.DataAccessLayer.Models;
using PetShelterCLI.PetShelterCLI;
using PetShelterCLI.PetShelterCLI.Domain;

var shelter = new PetShelterCLI.PetShelterCLI.Domain.PetShelter();

Console.WriteLine("Hello, Welcome the the Pet Shelter!");

var exit = false;
try
{
    while (!exit)
    {
        PresentOptions(
            "Here's what you can do.. ",
            new Dictionary<string, Action>
            {
                { "Register a newly rescued pet", RegisterPet },
                { "See our residents", SeePets },
                { "Donate", Donate },
                { "See current donations total", SeeDonations },
                { "Create a fundraiser", RegisterFundraiser },
                { "Donate to one of our fundraisers", ()=>ObserveFundraisers(name => DonateToFundraiser(name)) },
                { "Leave:(", Leave }
            }
        );
    }
}
catch (Exception e)
{
    Console.WriteLine($"Unfortunately we ran into an issue: {e.Message}.");
    Console.WriteLine("Please try again later.");
}


void RegisterPet()
{
    var name = ReadString("Name?");
    var description = ReadString("Description?");

    var pet = new Pet(name,description);

    shelter.RegisterPet(pet);
}

void Donate()
{
    Console.WriteLine("What's your name? (So we can credit you.)");
    var name = ReadString();

    Console.WriteLine("What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)");
    var id = ReadString();
    var person = new Person(name, id);
    var amount = ReadDouble("How much would you like to donate?");
    shelter.Donate((decimal)amount,person);
}

void RegisterFundraiser()
{
    var name = ReadString("Name?");
    var target = ReadDouble("Target donation?");

    var fundraiser = new Fundraiser(name, (decimal)target);
    shelter.RegisterFundraiser(fundraiser);
}

void SeePets()
{

    var pets = shelter.GetAllPets();

    var petOptions = new Dictionary<string, Action>();
    foreach (var pet in pets)
    {
        petOptions.Add(pet.Name, () => SeePetDetailsByName(pet.Name));
    }

    PresentOptions("We got..", petOptions);
}

void SeePetDetailsByName(string name)
{
    var pet = shelter.GetByName(name);
    Console.WriteLine($"A few words about {pet.Name}: {pet.Description}");
}

void SeeDonations()
{
    Console.WriteLine($"Our current donation total is {shelter.GetTotalDonations()}RON");
    Console.WriteLine("Special thanks to our donors:");
    var donors = shelter.GetAllDonors();
    foreach (var donor in donors)
    {
        Console.WriteLine(donor.Name);
    }
}


void ObserveFundraisers(Action<int> function)
{
    Console.WriteLine("Please choose one fundraiser from below: ");

    var fundraisers = shelter.GetAllFundraisers();

    var fundraisersOptions = new Dictionary<string, Action>();
    foreach (var f in fundraisers)
    {
        fundraisersOptions.Add(f.Name + " -- " + shelter.GetCurrentDonationFromFundraiser(f.Id).ToString("F2") + " Ron/" + f.Target + " Ron", () => function(f.Id));
    }
    fundraisersOptions.Add("Go back.", () => { });

    PresentOptions("We got..", fundraisersOptions);
}

void DonateToFundraiser(int fundraiserId)
{
    Fundraiser fundraiser = shelter.GetFundraiserById(fundraiserId);
    Console.WriteLine("What's your name? (So we can credit you.)");
    var name = ReadString();

    Console.WriteLine("What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)");
    var id = ReadString();
    var person = new Person(name, id);
    var amount = ReadDouble("How much would you like to donate?");
    shelter.Donate((decimal)amount, person, fundraiserId);

}


void Leave()
{
    Console.WriteLine("Good bye!");
    exit = true;
}

void PresentOptions(string header, IDictionary<string, Action> options)
{

    Console.WriteLine(header);

    for (var index = 0; index < options.Count; index++)
    {
        Console.WriteLine(index + 1 + ". " + options.ElementAt(index).Key);
    }
    var userInput = ReadInteger(options.Count);
    options.ElementAt(userInput - 1).Value();
}

string ReadString(string? header = null)
{
    if (header != null) Console.WriteLine(header);

    var value = Console.ReadLine();
    Console.WriteLine("");
    return value;
}


int ReadInteger(int maxValue = int.MaxValue, string? header = null)
{
    if (header != null) Console.WriteLine(header);

    var isUserInputValid = int.TryParse(Console.ReadLine(), out var userInput);
    if (!isUserInputValid || userInput > maxValue)
    {
        Console.WriteLine("Invalid input");
        Console.WriteLine("");
        return ReadInteger(maxValue, header);
    }

    Console.WriteLine("");
    return userInput;
}

double ReadDouble(string header, double maxValue = double.MaxValue)
{
    if (header != null) Console.WriteLine(header);

    var isUserInputValid = double.TryParse(Console.ReadLine(), out var userInput);
    if (!isUserInputValid || userInput > maxValue)
    {
        Console.WriteLine("Invalid input");
        Console.WriteLine("");
        return ReadDouble(header);
    }

    Console.WriteLine("");
    return userInput;
}