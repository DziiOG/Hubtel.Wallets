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
            : base(db, collectionName)
        {
            var WalletKey = Builders<Wallet>.IndexKeys
                .Ascending("Owner")
                .Ascending("AccountNumberHash");
            var indexOptions = new CreateIndexOptions { Unique = true };
            var Wallet = new CreateIndexModel<Wallet>(WalletKey, indexOptions);
            collection.Indexes.CreateOne(Wallet);
        }
    }
}
