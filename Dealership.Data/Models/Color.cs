using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Data.Models
{
    public class Color : Entity
    {
        private ICollection<Car> cars;

        public Color()
        {
            this.cars = new HashSet<Car>();
        }

        [Required]
        [MaxLength(25)]
        [MinLength(2)]
        public string Name { get; set; }

        public int ColorTypeId { get; set; }

        public ColorType ColorType { get; set; }

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
