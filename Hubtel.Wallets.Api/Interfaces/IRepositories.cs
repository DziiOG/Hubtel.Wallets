using System.Collections.Generic;
using System.Threading.Tasks;
using Agents.Api.Contracts.DataDtos;
using MongoDB.Bson;

namespace Agents.Api.Interfaces
{
    public interface IBaseRepository<T>
    {
        public Task<T> GetByIdAsync(ObjectId id);
        public Task<IEnumerable<T>> GetAsync(List<FilterCriteriaItem> criteria);

        public Task CreateAsync(T item);

        public Task UpdateAsync(T item);

        public Task DeleteAsync(ObjectId id);
        public Task PatchAsync(ObjectId Id, List<FilterCriteriaItem> criteria);
    }
}
