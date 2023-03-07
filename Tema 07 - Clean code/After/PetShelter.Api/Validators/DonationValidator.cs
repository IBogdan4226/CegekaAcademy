using FluentValidation;

using PetShelter.Api.Resources;

namespace PetShelter.Api.Validators
{
    public class DonationValidator : AbstractValidator<Resources.DonateRequiest>
    {
        
        public DonationValidator()
        {
            RuleFor(x => x.Donor).NotEmpty().SetValidator(new PersonValidator()); 
            RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
        }
    }
}
