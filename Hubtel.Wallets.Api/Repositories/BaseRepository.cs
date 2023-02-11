using System.Collections.Generic;
using Hubtel.Wallets.Api.Contracts.DataDtos;
using Hubtel.Wallets.Api.Interfaces;
using Hubtel.Wallets.Api.Helpers;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System;

namespace Hubtel.Wallets.Api.Repositories
{
    /// <summary>
    /// class contains repository methods for performing actions on model models
    /// </summary>
    public class BaseRepository<T> : IBaseRepository<T>
        where T : ModelWithId
    {
        /// <summary>
        /// model collection
        /// </summary>
        public readonly IMongoCollection<T> collection;

        /// <summary>
        /// filter builder for model model
        /// </summary>
        public readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;
        public readonly UpdateDefinitionBuilder<T> updateBuilder = Builders<T>.Update;

        /// <summary>
        /// initializes a new model repo
        /// </summary>
        public BaseRepository(IMongoDatabase db, string collectionName)
        {
            collection = db.GetCollection<T>(collectionName);
        }

        /// <summary>
        /// Creates a new model model
        /// </summary>
        /// <param name="model">model model to be created </param>
        /// <returns>Task</returns>
        public virtual async Task CreateAsync(T model)
        {
            await collection.InsertOneAsync(model);
        }

        /// <summary>
        /// Deletes an model model given an object id
        /// </summary>
        /// <param name="id">model object id</param>
        /// <returns>Task</returns>
        public virtual async Task DeleteAsync(ObjectId id)
        {
            var filter = filterBuilder.Eq(model => model.Id, id);
            await collection.DeleteOneAsync(filter);
        }

        /// <summary>
        /// Gets an model document given an id
        /// </summary>
        /// <param name="id">model object id</param>
        /// <returns>Task of an T </returns>
        public virtual async Task<T> GetByIdAsync(ObjectId id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(model => model.Id, id);
            return await collection.Find(filter).SingleOrDefaultAsync();
        }

        /// <summary>
        /// Gets all model based on a query
        /// </summary>
        /// <returns>Task of an IEmumerable of an T </returns>
        public virtual async Task<IEnumerable<T>> GetAsync(List<FilterCriteriaItem> criteria)
        {
            var query = Misc.QueryBuilder<T>(criteria);
            return await collection.Find(query).ToListAsync();
        }

        /// <summary>
        /// Updates an model model doc
        /// </summary>
        /// <param name="model">model object</param>
        /// <returns>Task</returns>
        public virtual async Task UpdateAsync(T model)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(
                existingItem => existingItem.Id,
                model.Id
            );
            await collection.ReplaceOneAsync(filter, model);
        }

        /// <summary>
        /// Updates an model model doc
        /// </summary>
        /// <param name="model">model object</param>
        /// <returns>Task</returns>
        public virtual async Task PatchAsync(ObjectId Id, List<FilterCriteriaItem> criteria)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(existingItem => existingItem.Id, Id);
            List<UpdateDefinition<T>> updateDefination = new List<UpdateDefinition<T>>();
            foreach (var criteriaItem in criteria)
            {
                updateDefination.Add(updateBuilder.Set(criteriaItem.Key, criteriaItem.Value));
            }
            updateDefination.Add(updateBuilder.Set("UpdatedDate", DateTimeOffset.UtcNow));
            UpdateDefinition<T> combinedUpdate = updateBuilder.Combine(updateDefination);
            await collection.UpdateOneAsync(filter, combinedUpdate);
        }
    }
}
