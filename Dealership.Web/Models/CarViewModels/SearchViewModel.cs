using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dealership.Web.Models.CarViewModels
{
    public class SearchViewModel
    {
        public SearchViewModel()
        {
        }

        public SearchResultViewModel SearchResult { get; set; }

        public List<SelectListItem> Brands { get; set; }
        public List<SelectListItem> CarModels { get; set; }
        public List<SelectListItem> SortCriterias { get; set; }
    }
}
