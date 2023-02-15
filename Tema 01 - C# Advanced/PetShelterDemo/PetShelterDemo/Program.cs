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

using PetShelterDemo.DAL;
using PetShelterDemo.Domain;

var shelter = new PetShelter();

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
                { "Donate", Donate },
                { "See current donations total", SeeDonations },
                { "See our residents", SeePets },
                { "Create a fundraiser", RegisterFundraiser },
                { "See our current fundraisers", ()=>ObserveFundraisers(name => ShowDetailsOfFundraiser(name))},
                { "Donate to one of our fundraisers", ()=>ObserveFundraisers(name => DonateToFundraiser(name)) },
                { "Break our database connection", BreakDatabaseConnection },
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

    var pet = new Pet(name, description);

    shelter.RegisterPet(pet);
}

void Donate()
{
    Console.WriteLine("What's your name? (So we can credit you.)");
    var name = ReadString();

    Console.WriteLine("What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)");
    var id = ReadString();
    var person = new Person(name, id);

    Console.WriteLine("How much would you like to donate? (Select currency then write amount)");
    shelter.Donate(person);
}

void SeeDonations()
{
    Console.WriteLine($"Our current donation total is {shelter.GetTotalDonationsInRON()}RON");
    Console.WriteLine("Special thanks to our donors:");
    var donors = shelter.GetAllDonors();
    foreach (var donor in donors)
    {
        Console.WriteLine(donor.Name);
    }
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


#region HOMEWORK Tema 01 - C# Advanced
void RegisterFundraiser()
{
    var name = ReadString("Name?");
    var description = ReadString("Description?");
    var target = ReadDouble("Target donation in RON? Keep in mind that people will be able to donate in other currencies.");

    var fundraiser = new Fundraiser(name, description, target);
    shelter.RegisterFundraiser(fundraiser);
}


void ObserveFundraisers(Action<string> function)
{
    Console.WriteLine("Please choose one fundraiser from below: ");

    var fundraisers = shelter.GetAllFundraisers();

    var fundraisersOptions = new Dictionary<string, Action>();
    foreach (var f in fundraisers)
    {
        fundraisersOptions.Add(f.Name + " -- " + f.MoneyRaised.CalculateValue().ToString("F2") + " Ron/" + f.Target+" Ron", () => function(f.Name));
    }
    fundraisersOptions.Add("Go back.", () => { });

    PresentOptions("We got..", fundraisersOptions);
}


void DonateToFundraiser(string fundraiserName)
{
    Fundraiser fundraiser = shelter.GetByNameFundraiser(fundraiserName);
    Console.WriteLine("What's your name? (So we can credit you.)");
    var name = ReadString();

    Console.WriteLine("What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)");
    var id = ReadString();
    var person = new Person(name, id);

    var currency = Donation.GetCurrencyChoice();
    var amount = Donation.GetDonationAmount();
    shelter.Donate(person, currency, amount, fundraiser);

}


void ShowDetailsOfFundraiser(String name)
{
    Fundraiser fundraiser = shelter.GetByNameFundraiser(name);

    Console.WriteLine(fundraiser.Name);
    Console.WriteLine(fundraiser.Description);
    Console.WriteLine($"Out of the {fundraiser.Target} RON we managed to get {fundraiser.MoneyRaised.CalculateValue():F2} RON");
    Console.WriteLine("Special thanks to: ");
    foreach (var donor in fundraiser.getDonors())
    {
        Console.WriteLine(donor.Name);
    }
    Console.WriteLine("");

}
#endregion


void BreakDatabaseConnection()
{
    Database.ConnectionIsDown = true;
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