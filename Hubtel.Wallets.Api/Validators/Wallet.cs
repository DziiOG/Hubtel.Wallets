using Hubtel.Wallets.Api.Contracts.RequestDtos;
using FluentValidation;
using Hubtel.Wallets.Api.Helpers;

namespace Hubtel.Wallets.Api.Validators
{
    public class CreateWalletValidator : AbstractValidator<CreateWalletDto>
    {
        public CreateWalletValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Type is required.")
                .Must(x => x == "momo" || x == "card")
                .WithMessage("Type must be either 'momo' or 'card'.");

            RuleFor(x => x.AccountNumber)
                .NotEmpty()
                .WithMessage("AccountNumber is required.")
                .When(x => x.Type == "momo")
                .Must(x => Misc.IsValidPhoneNumber(x))
                .WithMessage(
                    "AccountNumber must be a valid phone number if Type is 'momo' or a valid card number if Type is 'card'."
                )
                .When(x => x.Type == "card")
                .Must(x => Misc.IsValidCardNumber(x))
                .When(x => x.Type == "card")
                .MinimumLength(6)
                .When(x => x.Type == "card")
                .MaximumLength(16)
                .WithMessage(
                    "AccountNumber must be a valid phone number if Type is 'momo' or a valid card number if Type is 'card'."
                );

            RuleFor(x => x.AccountScheme)
                .NotEmpty()
                .WithMessage("AccountScheme is required.")
                .When(x => x.Type == "momo")
                .Must(x => Misc.IsValidMomoScheme(x))
                .When(x => x.Type == "card")
                .Must(x => Misc.IsValidCardScheme(x));

            RuleFor(x => x.Owner)
                .NotEmpty()
                .WithMessage("Owner is required.")
                .Must(x => Misc.IsValidPhoneNumber(x))
                .WithMessage("Owner must be a valid phone number.");
        }
    }

    public class PatchWalletValidator : AbstractValidator<PatchWalletDto>
    {
        public PatchWalletValidator()
        {
            RuleFor(x => x.Name);
            RuleFor(x => x.Type)
                .Must(x => !string.IsNullOrEmpty(x) ? x == "momo" || x == "card" : true)
                .WithMessage("Type must be either 'momo' or 'card'.");

            RuleFor(x => x.AccountNumber)
                .Must(x => !string.IsNullOrEmpty(x) ? Misc.IsValidPhoneNumber(x) : true)
                .When(x => x.Type == "momo")
                .WithMessage(
                    "AccountNumber must be a valid phone number if Type is 'momo' or a valid card number if Type is 'card'."
                )
                .Must(x => !string.IsNullOrEmpty(x) ? Misc.IsValidCardNumber(x) : true)
                .When(x => x.Type == "card")
                .WithMessage(
                    "AccountNumber must be a valid phone number if Type is 'momo' or a valid card number if Type is 'card'."
                );

            RuleFor(x => x.AccountScheme)
                .Must(x => !string.IsNullOrEmpty(x) ? Misc.IsValidMomoScheme(x) : true)
                .When(x => x.Type == "momo")
                .WithMessage(
                    "AccountScheme must be a valid momo scheme if Type is 'momo' or a valid card scheme if Type is 'card'."
                )
                .Must(x => !string.IsNullOrEmpty(x) ? Misc.IsValidCardScheme(x) : true)
                .When(x => x.Type == "card")
                .WithMessage(
                    "AccountScheme must be a valid momo scheme if Type is 'momo' or a valid card scheme if Type is 'card'."
                );

            RuleFor(x => x.Owner)
                .Must(x => !string.IsNullOrEmpty(x) ? Misc.IsValidPhoneNumber(x) : true)
                .WithMessage("Owner must be a valid phone number.");
        }
    }

    public class QueryWalletValidator : AbstractValidator<WalletQueryDto>
    {
        public QueryWalletValidator()
        {
            RuleFor(x => x.AccountScheme)
                .Must(x => !string.IsNullOrEmpty(x) ? Misc.IsValidMomoScheme(x) : true)
                .When(x => x.Type == "momo")
                .WithMessage(
                    "AccountScheme must be a valid momo scheme if Type is 'momo' or a valid card scheme if Type is 'card'."
                )
                .Must(x => !string.IsNullOrEmpty(x) ? Misc.IsValidCardScheme(x) : true)
                .When(x => x.Type == "card")
                .WithMessage(
                    "AccountScheme must be a valid momo scheme if Type is 'momo' or a valid card scheme if Type is 'card'."
                );

            RuleFor(x => x.Type)
                .Must(x => !string.IsNullOrEmpty(x) ? x == "momo" || x == "card" : true)
                .WithMessage("Type must be either 'momo' or 'card'.");
            RuleFor(x => x.Owner)
                .Must(x => !string.IsNullOrEmpty(x) ? Misc.IsValidPhoneNumber(x) : true)
                .WithMessage("Owner must be a valid phone number.");
        }
    }
}
