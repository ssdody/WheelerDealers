using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Data.Models
{
    public class Extra : Entity
    {
        private ICollection<CarsExtras> carsExtras;

        public Extra()
        {
            this.carsExtras = new HashSet<CarsExtras>();
        }

        [Required]
        [MaxLength(25)]
        [MinLength(2)]
        public string Name { get; set; }

        public ICollection<CarsExtras> CarsExtras
        {
            get { return this.carsExtras; }
            set
            {
                this.carsExtras = value;
            }
        }
    }
}
