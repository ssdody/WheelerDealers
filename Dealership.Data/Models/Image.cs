namespace Dealership.Data.Models
{
    public class Image : Entity
    {
        public string ImageName { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
