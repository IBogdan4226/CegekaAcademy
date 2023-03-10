using PetShelter.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.BusinessLayer.BuilderDesignPattern;

public interface IRescuePetDirector
{
    RescuePetRequest ConstructCuteCat();
    RescuePetRequest ConstructScaryDog();
}
