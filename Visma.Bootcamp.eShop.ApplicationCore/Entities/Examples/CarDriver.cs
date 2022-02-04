using System.ComponentModel.DataAnnotations;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Examples
{
    internal class CarDriver
    {
        [Required]
        public int CarId { get; set; }
        public Car Car { get; set; }

        [Required]
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}
