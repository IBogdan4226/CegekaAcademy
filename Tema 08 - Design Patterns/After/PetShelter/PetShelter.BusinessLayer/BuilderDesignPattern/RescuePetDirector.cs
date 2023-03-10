using PetShelter.BusinessLayer.Constants;
using PetShelter.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.BusinessLayer.BuilderDesignPattern;

public class RescuePetDirector
{
    private readonly IRescuePetBuilder _builder;

    public RescuePetDirector(IRescuePetBuilder builder)
    {
        _builder = builder;
    }

    public RescuePetRequest ConstructCuteCat()
    {
        return _builder
            .SetPetName("Whiskers")
            .SetDescription("A cute and cuddly cat")
            .SetPetType(PetType.Cat)
            .SetIsHealthy(true)
            .SetWeightInKg(3.2m)
            .SetImageUrl("https://wallpaper.dog/large/10838032.jpg")
            .SetPerson(new Person { Name = "Jane Doe", IdNumber = "1234567890123", DateOfBirth = new DateTime(2000, 9, 5) })
            .Build();
    }

    public RescuePetRequest ConstructScaryDog()
    {
        return _builder
            .SetPetName("Fang")
            .SetDescription("A scary looking dog well trained with a playful heart.")
            .SetPetType(PetType.Dog)
            .SetIsHealthy(true)
            .SetWeightInKg(20.0m)
            .SetImageUrl("https://publish.purewow.net/wp-content/uploads/sites/2/2021/07/big-dog-breeds-alaskan-malamute.jpg?fit=728%2C524")
            .SetPerson(new Person { Name = "Kendrick Lamar", IdNumber = "1234567890000", DateOfBirth = new DateTime(1987, 6, 17) })
            .Build();
    }
}
