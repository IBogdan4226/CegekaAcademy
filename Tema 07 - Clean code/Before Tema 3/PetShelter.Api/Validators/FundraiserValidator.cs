using FluentValidation;

using PetShelter.Api.Resources;

namespace PetShelter.Api.Validators
{
    public class FundraiserValidator : AbstractValidator<Resources.AddedFundraiser>
    {

        public FundraiserValidator()
        {
            RuleFor(x => x.Target).NotEmpty().GreaterThan(0);
            RuleFor(x => x.DueDate).NotEmpty().GreaterThan(DateTime.Now);
            RuleFor(x => x.Owner).NotEmpty().SetValidator(new PersonValidator());
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
        }
    }
}
