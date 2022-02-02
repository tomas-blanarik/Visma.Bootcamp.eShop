using System.ComponentModel.DataAnnotations;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Models
{
    public class ProductModel
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
