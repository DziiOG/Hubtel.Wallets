using System.Collections.Generic;
using Hubtel.Wallets.Api.Contracts.DataDtos;
using System.ComponentModel;
using System;

namespace Hubtel.Wallets.Api.Extensions
{
    public static class ObjectToDictionaryHelper
    {
        public static IDictionary<string, object> ToDictionary(this object source)
        {
            return source.ToDictionary<object>();
        }

        public static List<FilterCriteriaItem> ToFilterCriteria(
            this object source,
            IDictionary<string, string> types = null
        )
        {
            if (source == null)
            {
                ThrowExceptionWhenSourceArgumentIsNull();
            }

            var filterCriteria = new FilterCriteria();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source!))
            {
                object value = property.GetValue(source);
                string key = property.Name;
                if (value != null)
                {
                    if (value is string && !string.IsNullOrEmpty((string)value))
                    {
                        ToFilterCriteriaItem(types, filterCriteria, value, key);
                    }
                    else if (value is string && string.IsNullOrEmpty((string)value)) { }
                    else if (value is not string)
                    {
                        ToFilterCriteriaItem(types, filterCriteria, value, key);
                    }
                }
            }

            return filterCriteria.criteria;
        }

        private static void ToFilterCriteriaItem(
            IDictionary<string, string> types,
            FilterCriteria filterCriteria,
            object value,
            string key
        )
        {
            FilterCriteriaItem newFilterCriteriaItem = new FilterCriteriaItem();
            newFilterCriteriaItem.Key = key;
            newFilterCriteriaItem.Value = value;
            if (types != null)
            {
                string _type;
                bool hasType = types.TryGetValue(key, out _type);
                if (hasType && _type != null)
                {
                    newFilterCriteriaItem.Type = _type;
                }
                else
                {
                    newFilterCriteriaItem.Type = "eq";
                }
            }
            else
            {
                newFilterCriteriaItem.Type = "eq";
            }
            filterCriteria.criteria.Add(newFilterCriteriaItem);
        }

        public static IDictionary<string, T> ToDictionary<T>(this object source)
        {
            if (source == null)
            {
                ThrowExceptionWhenSourceArgumentIsNull();
            }

            var dictionary = new Dictionary<string, T>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source!))
                AddPropertyToDictionary<T>(property, source!, dictionary);
            return dictionary;
        }

        private static void AddPropertyToDictionary<T>(
            PropertyDescriptor property,
            object source,
            Dictionary<string, T> dictionary
        )
        {
            object value = property!.GetValue(source!)!;
            if (IsOfType<T>(value))
                dictionary.Add(property.Name, (T)value);
        }

        private static bool IsOfType<T>(object value)
        {
            return value is T;
        }

        private static void ThrowExceptionWhenSourceArgumentIsNull()
        {
            throw new ArgumentNullException(
                "source",
                "Unable to convert object to a dictionary. The source object is null."
            );
        }
    }
}
