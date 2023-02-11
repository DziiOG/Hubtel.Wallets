using Hubtel.Wallets.Api.Contracts.RequestDtos;
using Hubtel.Wallets.Api.Helpers;
using FluentValidation;

namespace Hubtel.Wallets.Api.Validators
{
    public class ObjectIdValidator : AbstractValidator<string>
    {
        public ObjectIdValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .NotNull()
                .Must(Misc.IsValidObjectId)
                .WithMessage("Provide valid Id");
        }
    }
}
