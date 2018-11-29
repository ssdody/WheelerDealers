using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Data.Models
{
    public class Brand : Entity
    {
        private ICollection<Car> cars;
        private ICollection<CarModel> carModels;

        public Brand()
        {
            this.cars = new HashSet<Car>();
            this.carModels = new HashSet<CarModel>();
        }

        [Required]
        [MaxLength(25)]
        [MinLength(2)]
        public string Name { get; set; }
        public ICollection<CarModel> CarModels
        {
            get { return this.carModels; }
            set
            {
                this.carModels = value;
            }
        }


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
