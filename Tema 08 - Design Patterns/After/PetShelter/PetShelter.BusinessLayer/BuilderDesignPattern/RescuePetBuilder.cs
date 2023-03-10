using PetShelter.BusinessLayer.Constants;
using PetShelter.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.BusinessLayer.BuilderDesignPattern;

public class RescuePetBuilder: IRescuePetBuilder
{
    private RescuePetRequest _request;

    public RescuePetBuilder()
    {
        this.Reset();
    }
    public IRescuePetBuilder SetPetName(string petName)
    {
        _request.PetName = petName;
        return this;
    }

    public IRescuePetBuilder SetDescription(string description)
    {
        _request.Description = description;
        return this;
    }

    public IRescuePetBuilder SetPetType(PetType type)
    {
        _request.Type = type;
        return this;
    }

    public IRescuePetBuilder SetIsHealthy(bool isHealthy)
    {
        _request.IsHealthy = isHealthy;
        return this;
    }

    public IRescuePetBuilder SetWeightInKg(decimal weightInKg)
    {
        _request.WeightInKg = weightInKg;
        return this;
    }

    public IRescuePetBuilder SetImageUrl(string imageUrl)
    {
        _request.ImageUrl = imageUrl;
        return this;
    }

    public IRescuePetBuilder SetPerson(Person person)
    {
        _request.Person = person;
        return this;
    }

    public RescuePetRequest Build()
    {
        return _request;
    }
    public void Reset()
    {
        _request=new RescuePetRequest();
    }
}
