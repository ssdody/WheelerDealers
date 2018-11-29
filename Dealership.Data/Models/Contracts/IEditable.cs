using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models.Contracts
{
    public interface IEditable
    {
        DateTime? CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
