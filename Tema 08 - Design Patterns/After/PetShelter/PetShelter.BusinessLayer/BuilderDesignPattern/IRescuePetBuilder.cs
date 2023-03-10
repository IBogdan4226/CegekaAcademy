using PetShelter.BusinessLayer.Constants;
using PetShelter.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.BusinessLayer.BuilderDesignPattern;

public interface IRescuePetBuilder
{
    IRescuePetBuilder SetPetName(string petName);
    IRescuePetBuilder SetDescription(string description);
    IRescuePetBuilder SetPetType(PetType type);
    IRescuePetBuilder SetIsHealthy(bool isHealthy);
    IRescuePetBuilder SetWeightInKg(decimal weightInKg);
    IRescuePetBuilder SetImageUrl(string imageUrl);
    IRescuePetBuilder SetPerson(Person person);
    RescuePetRequest Build();
    void Reset();
}