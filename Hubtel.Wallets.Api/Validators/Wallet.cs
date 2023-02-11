using Hubtel.Wallets.Api.Contracts.RequestDtos;
using FluentValidation;
using Hubtel.Wallets.Api.Helpers;

namespace Hubtel.Wallets.Api.Validators
{
    public class CreateWalletValidator : AbstractValidator<CreateWalletDto>
    {
        public CreateWalletValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Type of wallet is required")
                .Must(x => x == "momo" || x == "card")
                .WithMessage("Type must be either 'momo' or 'card'");

            RuleFor(x => x.AccountNumber)
                .Must(x => x.Length >= 6 && x.Length <= 16)
                .When(x => x.Type == "card")
                .WithMessage("Card number must be between 6 and 16 characters")
                .Must(x => Misc.IsValidCardNumber(x))
                .When(x => x.Type == "card")
                .WithMessage("Invalid card number");

            RuleFor(x => x.AccountNumber)
                .Must(x => Misc.IsValidPhoneNumber(x))
                .When(x => x.Type == "momo")
                .WithMessage("Invalid phone number");

            RuleFor(x => x.AccountScheme)
                .Must(x => x == "mtn" || x == "airteltigo" || x == "vodafone")
                .When(x => x.Type == "momo")
                .WithMessage("Momo must be one of 'mtn', 'airteltigo', 'vodafone'");

            RuleFor(x => x.AccountScheme)
                .Must(x => x == "visa" || x == "mastercard")
                .When(x => x.Type == "card")
                .WithMessage("Card must be one of 'visa', 'mastercard'");

            RuleFor(x => x.Owner)
                .NotEmpty()
                .WithMessage("User phone number is required")
                .Must(x => Misc.IsValidPhoneNumber(x))
                .WithMessage("Invalid phone number");
        }
    }

    public class PatchWalletValidator : AbstractValidator<PatchWalletDto>
    {
        public PatchWalletValidator() { }
    }

    public class QueryWalletValidator : AbstractValidator<WalletQueryDto>
    {
        public QueryWalletValidator()
        {
            RuleFor(x => x.Owner);
            // .Must(x => !string.IsNullOrEmpty(x) ? Misc.IsValidPhoneNumber(x) : true)
            // .WithMessage("Owner must be a valid phone number.");
        }
    }
}
