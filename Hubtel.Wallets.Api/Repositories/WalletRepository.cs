using Hubtel.Wallets.Api.Interfaces;
using Hubtel.Wallets.Api.Models;
using MongoDB.Driver;

namespace Hubtel.Wallets.Api.Repositories
{
    /// <summary>
    /// class contains repository methods for performing actions on wallet models
    /// </summary>
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        private const string collectionName = "wallets";

        public WalletRepository(IMongoDatabase db)
            : base(db, collectionName) { }
    }
}
