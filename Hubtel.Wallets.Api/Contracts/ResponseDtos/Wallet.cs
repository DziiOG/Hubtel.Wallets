using System;

namespace Hubtel.Wallets.Api.Contracts.ResponseDtos
{
    public record WalletDto
    {
        public string Id { set; get; } = null!;
        public string Name { set; get; } = null!;

        public string Type { set; get; } = null!;

        public string AccountNumber { set; get; } = null!;

        public string AccountScheme { set; get; } = null!;

        public string Owner { set; get; } = null!;

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
