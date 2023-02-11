using Hubtel.Wallets.Api.Contracts.RequestDtos;
using FluentValidation;

namespace Hubtel.Wallets.Api.Validators
{
    public class CreateWalletValidator : AbstractValidator<CreateWalletDto>
    {
        public CreateWalletValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.AccountScheme).NotNull().NotEmpty();
            RuleFor(x => x.Type).NotEmpty().NotNull();
            RuleFor(x => x.AccountNumber).NotNull().NotEmpty();
            RuleFor(x => x.Owner).NotNull().NotEmpty();
        }
    }

    public class PatchWalletValidator : AbstractValidator<PatchWalletDto>
    {
        public PatchWalletValidator()
        {
            RuleFor(x => x.Name);
            RuleFor(x => x.AccountNumber);
            RuleFor(x => x.AccountScheme);
            RuleFor(x => x.Type);
            RuleFor(x => x.Owner);
        }
    }

    public class QueryWalletValidator : AbstractValidator<WalletQueryDto>
    {
        public QueryWalletValidator()
        {
            RuleFor(x => x.AccountScheme);
            RuleFor(x => x.Type);
            RuleFor(x => x.Owner);
        }
    }
}
