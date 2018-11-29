﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Data.Models
{
    public class FuelType : Entity
    {
        private ICollection<Car> cars;

        public FuelType()
        {
            this.cars = new HashSet<Car>();
        }

        [Required]
        [MaxLength(25)]
        [MinLength(2)]
        public string Name { get; set; }
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
