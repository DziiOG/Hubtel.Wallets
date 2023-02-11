using System.Collections.Generic;
using Hubtel.Wallets.Api.Contracts.ResponseDtos;
using Hubtel.Wallets.Api.Contracts.DataDtos;
using FluentValidation.Results;
using Hubtel.Wallets.Api.Settings;
using FluentValidation;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Hubtel.Wallets.Api.Helpers
{
    /// <summary>
    /// class methods that provide some basic helpful functionalities
    /// </summary>
    public static class Misc
    {
        /// <summary>
        /// Methods is a string is a valid ObjectId from MongoId
        /// </summary>
        /// <param name="Id">string of id</param>
        /// <returns>bool</returns>
        public static bool IsValidObjectId(string Id)
        {
            return ObjectId.TryParse(Id, out _);
        }

        /// <summary>
        /// Method constructs a query build for mongodb
        /// </summary>
        /// <param name="filterCriteria">list of filter criterias</param>
        /// <returns>bool</returns>
        public static FilterDefinition<T> QueryBuilder<T>(List<FilterCriteriaItem> filterCriteria)
        {
            FilterDefinitionBuilder<T> filter = Builders<T>.Filter;
            var query = filter.Empty;
            foreach (var filterCriteriaItem in filterCriteria)
            {
                switch (filterCriteriaItem.Type)
                {
                    case "eq":
                        query &= filter.Eq(filterCriteriaItem.Key, filterCriteriaItem.Value);
                        break;
                    case "gt":
                        query &= filter.Gt(filterCriteriaItem.Key, filterCriteriaItem.Value);
                        break;
                }
            }

            return query;
        }
    }
}
