using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Dealership.Web.Models.CarViewModels
{
    public class BrowseViewModel
    {
        public BrowseViewModel()
        {
        }

        public IEnumerable<CarSummaryViewModel> Summaries { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }

        public int ResultsCount { get; set; }

        public int SelectedBrandId { get; set; }
        public int SelectedModelId { get; set; }

        public int Sort { get; set; }

        public List<SelectListItem> Brands { get; set; }
        public List<SelectListItem> CarModels { get; set; }
        public List<SelectListItem> SortCriterias { get; set; }
    }
}
