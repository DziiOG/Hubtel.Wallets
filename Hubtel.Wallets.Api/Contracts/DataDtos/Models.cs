using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Hubtel.Wallets.Api.Contracts.DataDtos
{
    public class ModelWithId : object
    {
        [BsonId]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
