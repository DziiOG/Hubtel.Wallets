using System.Collections.Generic;

namespace Agents.Api.Contracts.DataDtos
{
    public class FilterCriteriaItem
    {
        public string Key { get; set; } = null!;
        public object Value { get; set; } = null!;
        public string Type { get; set; } = null!;
    }

    public class FilterCriteria
    {
        public List<FilterCriteriaItem> criteria = new List<FilterCriteriaItem>() { };
    }
}
