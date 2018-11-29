using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Data.Models
{
    public class Gearbox : Entity
    {
        private ICollection<Car> cars;

        public Gearbox()
        {
            this.cars = new HashSet<Car>();
        }

        public int GearTypeId { get; set; }

        [Range(1, 10)]
        public byte NumberOfGears { get; set; }

        public GearType GearType { get; set; }

        public ICollection<Car> Cars
        {
            get { return this.cars; }
            set
            {
                this.cars = value;
            }
        }
    }
}
