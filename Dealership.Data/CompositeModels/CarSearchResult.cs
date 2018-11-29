using System.Collections.Generic;

namespace Dealership.Data.CompositeModels
{
    public class CarSearchResult
    {
        public IEnumerable<CarSummary> FoundCars { get; set; }
        public int TotalCount { get; set; }
    }
}
