using System;
using Hubtel.Wallets.Api.Interfaces;
using Hubtel.Wallets.Api.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Hubtel.Wallets.Api.Intallers
{
    /// <summary>
    /// class installs mongo db settings, services
    /// </summary>
    public class MongoDBInstaller : IInstaller
    {
        /// <summary>
        /// Methods creates a connection to mongo db and then adds it as a singleton aslo configures the mongo to accept guid as object default _id
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        /// <returns>void</returns>
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<MongoDB.Driver.IMongoDatabase>(serviceProvide =>
            {
                MongoDB.Bson.Serialization.BsonSerializer.RegisterSerializer(
                    new MongoDB.Bson.Serialization.Serializers.GuidSerializer(
                        MongoDB.Bson.BsonType.String
                    )
                );
                MongoDB.Bson.Serialization.BsonSerializer.RegisterSerializer(
                    new MongoDB.Bson.Serialization.Serializers.DateTimeOffsetSerializer(
                        MongoDB.Bson.BsonType.String
                    )
                );
                MongoDbSettings mongoDbSettings = new MongoDbSettings();

                configuration.Bind(key: nameof(mongoDbSettings), mongoDbSettings);

                string host = mongoDbSettings.Host;
                string port = mongoDbSettings.Port;
                string dbName = mongoDbSettings.DatabaseName;

                MongoDbSettings settings = new MongoDbSettings()
                {
                    Host = host,
                    Port = port,
                    DatabaseName = dbName,
                };
                IMongoClient client = new MongoDB.Driver.MongoClient(settings.ConnectionString);
                client.WithReadConcern(MongoDB.Driver.ReadConcern.Majority);
                client.WithWriteConcern(MongoDB.Driver.WriteConcern.WMajority);
                return client.GetDatabase(dbName);
            });
        }
    }
}
