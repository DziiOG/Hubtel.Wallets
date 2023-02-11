using System.Collections.Generic;
using Hubtel.Wallets.Api.Contracts.ResponseDtos;
using Hubtel.Wallets.Api.Contracts.DataDtos;
using FluentValidation.Results;
using Hubtel.Wallets.Api.Settings;
using FluentValidation;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.RegularExpressions;
using System;
using System.Security.Cryptography;

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

        private static readonly Regex PhoneNumberPattern = new Regex(@"^\+(?:[0-9] ?){6,14}[0-9]$");

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            return PhoneNumberPattern.IsMatch(phoneNumber);
        }

        public static bool IsValidCardNumber(string cardNumber)
        {
            return Regex.Match(cardNumber.Substring(0, 6), @"^\d{6}$").Success;
        }

        public static bool IsValidMomoScheme(string value)
        {
            Console.WriteLine(value);
            return value == "mtn" || value == "airteltigo" || value == "vodafone";
        }

        public static bool IsValidCardScheme(string value)
        {
            return value == "visa" || value == "mastercard";
        }

        public static string GenerateHash(string cardNumber)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] hash = sha256Hash.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(cardNumber)
                );
                string cardNumberHash = BitConverter.ToString(hash).Replace("-", string.Empty);
                return cardNumberHash;
            }
        }
    }
}
