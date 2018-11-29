using Dealership.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dealership.Web.Areas.Admin.Models
{
    public class ModelViewModel
    {
        public int BrandId { get; set; }
        public string ModelName { get; set; }
        public IList<SelectListItem> Brands { get; set; }

        public ModelViewModel()
        {

        }
    }
}
