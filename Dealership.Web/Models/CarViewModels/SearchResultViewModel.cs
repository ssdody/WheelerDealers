using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dealership.Web.Models.CarViewModels
{
    public class SearchResultViewModel
    {

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }

        public int SelectedBrandId { get; set; }

        public int SelectedModelId { get; set; }

        public int Sort { get; set; }

        public IEnumerable<CarSummaryViewModel> Summaries { get; set; }

    }
}
