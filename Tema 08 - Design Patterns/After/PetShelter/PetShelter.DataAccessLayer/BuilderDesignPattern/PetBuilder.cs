using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.BuilderDesignPattern;

public class PetBuilder: IPetBuilder
{
    private Pet _request;

    public PetBuilder()
    {
        this.Reset();
    }
    public IPetBuilder SetPetName(string petName)
    {
        _request.Name = petName;
        return this;
    }

    public IPetBuilder SetDescription(string description)
    {
        _request.Description = description;
        return this;
    }

    public IPetBuilder SetPetType(string type)
    {
        _request.Type = type;
        return this;
    }

    public IPetBuilder SetIsHealthy(bool isHealthy)
    {
        _request.IsHealthy = isHealthy;
        return this;
    }

    public IPetBuilder SetWeightInKg(decimal weightInKg)
    {
        _request.WeightInKg = weightInKg;
        return this;
    }

    public IPetBuilder SetImageUrl(string imageUrl)
    {
        _request.ImageUrl = imageUrl;
        return this;
        
    }
    public Pet Build()
    {
        return _request;
    }
    public void Reset()
    {
        _request=new Pet();
    }
}
