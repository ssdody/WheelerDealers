using Dealership.Data.Models.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Data.Models
{
    public class CarsExtras : IDeletable, IEditable
    {
        public int CarId { get; set; }
        public virtual Car Car { get; set; }

        public int ExtraId { get; set; }
        public virtual Extra Extra { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
    }
}
