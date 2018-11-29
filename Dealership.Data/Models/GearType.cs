using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Data.Models
{
    public class GearType : Entity
    {
        private ICollection<Gearbox> gearboxes;

        public GearType()
        {
            this.gearboxes = new HashSet<Gearbox>();
        }

        [Required]
        [MaxLength(25)]
        [MinLength(2)]
        public string Name { get; set; }

        public ICollection<Gearbox> Gearboxes
        {
            get { return this.gearboxes; }
            set
            {
                this.gearboxes = value;
            }
        }
    }
}
