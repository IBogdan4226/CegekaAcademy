using FluentValidation;
using PetShelter.BusinessLayer.Constants;
using PetShelter.BusinessLayer.Models;

namespace PetShelter.BusinessLayer.Validators;

public class PersonValidator: AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(x => x.IdNumber).Length(PersonConstants.IdNumberLength);
        RuleFor(x => x.Name).NotEmpty().MinimumLength(PersonConstants.NameMinLength);
        RuleFor(x => x.DateOfBirth).NotEmpty()
            .Must(dateOfBirth => BeAboveMinimumAge(dateOfBirth ?? DateTime.Now))
            .WithMessage($"The person must be at least {PersonConstants.AdultMinAge} years old.");
    }



    private bool BeAboveMinimumAge(DateTime dateOfBirth)
    {
        var today = DateTime.Now;
        var donors18yoBirthday = dateOfBirth.AddYears(PersonConstants.AdultMinAge);
       
        return today >= donors18yoBirthday;
    }
}