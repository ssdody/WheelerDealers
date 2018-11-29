using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Dealership.Web.Models
{
    public class EditCarViewModel
    {
        public CarViewModel Car { get; set; }

        public List<SelectListItem> Brands { get; set; }

        public List<SelectListItem> CarModels { get; set; }

        public List<SelectListItem> GearTypes { get; set; }

        public List<SelectListItem> NumberOfGears { get; set; }

        public List<SelectListItem> BodyTypes { get; set; }

        public List<SelectListItem> ColorTypes { get; set; }

        public List<SelectListItem> FuelTypes { get; set; }

        public ICollection<IFormFile> Images
        {
            get
            {
                return this.Car.Images;
            }
            set
            {
                this.Car.Images = value;
            }
        }

        public ExtraCheckBox[] Extras { get; set; }
    }

    public class ExtraCheckBox
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}
