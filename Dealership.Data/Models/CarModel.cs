using System.Collections.Generic;

namespace Dealership.Data.Models
{
    public class CarModel : Entity
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
