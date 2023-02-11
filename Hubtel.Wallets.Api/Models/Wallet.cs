using System.ComponentModel.DataAnnotations;
using Hubtel.Wallets.Api.Contracts.DataDtos;

namespace Hubtel.Wallets.Api.Models
{
    public class Wallet : ModelWithId
    {
        [Required]
        public string Name { set; get; } = null!;

        [Required]
        public string Type { set; get; } = null!;

        [Required]
        public string AccountNumberHash { set; get; } = null!;

        [Required]
        public string AccountScheme { set; get; } = null!;

        [Required]
        public string Owner { set; get; } = null!;
    }
}
