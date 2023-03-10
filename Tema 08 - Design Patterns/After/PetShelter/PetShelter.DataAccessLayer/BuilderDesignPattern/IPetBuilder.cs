using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.BuilderDesignPattern;

public interface IPetBuilder
{
    IPetBuilder SetPetName(string petName);
    IPetBuilder SetDescription(string description);
    IPetBuilder SetPetType(String type);
    IPetBuilder SetIsHealthy(bool isHealthy);
    IPetBuilder SetWeightInKg(decimal weightInKg);
    IPetBuilder SetImageUrl(string imageUrl);
    Pet Build();
    void Reset();
}