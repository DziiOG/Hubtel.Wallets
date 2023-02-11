namespace Hubtel.Wallets.Api.Contracts.RequestDtos
{
    public record WalletQueryDto
    {
        public string? Type { set; get; } = null!;

        public string? AccountScheme { set; get; } = null!;

        public string? Owner { set; get; } = null!;
    }

    public record CreateWalletDto
    {
        public string Name { set; get; } = null!;

        public string Type { set; get; } = null!;

        public string AccountNumber { set; get; } = null!;

        public string AccountScheme { set; get; } = null!;

        public string Owner { set; get; } = null!;
    }

    public record PatchWalletDto
    {
        public string? Name { set; get; } = null!;

        public string? Type { set; get; } = null!;

        public string? AccountNumber { set; get; } = null!;

        public string? AccountScheme { set; get; } = null!;

        public string? Owner { set; get; } = null!;
    }
}
