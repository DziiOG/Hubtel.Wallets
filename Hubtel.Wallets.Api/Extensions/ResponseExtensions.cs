using Hubtel.Wallets.Api.Contracts.ResponseDtos;
using Hubtel.Wallets.Api.Models;

namespace Hubtel.Wallets.Api.Extensions
{
    /// <summary>
    /// static class for dtos extension
    /// </summary>
    public static class ResponseExtensions
    {
        /// <summary>
        /// Method creates a new wallet dto object
        /// </summary>
        /// <param name="wallet">==reference that defines the schema for wallet model;</param>
        /// <returns>WalletDto</returns>
        public static WalletDto AsDto(this Wallet wallet)
        {
            return new WalletDto
            {
                Id = wallet.Id.ToString(),
                Name = wallet.Name,
                Type = wallet.Type,
                AccountNumber = wallet.AccountNumber,
                AccountScheme = wallet.AccountScheme,
                Owner = wallet.Owner,
                CreatedDate = wallet.CreatedDate,
                UpdatedDate = wallet.UpdatedDate,
            };
        }
    }
}
