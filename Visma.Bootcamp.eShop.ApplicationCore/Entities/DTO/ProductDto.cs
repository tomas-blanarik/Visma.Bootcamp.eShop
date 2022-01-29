using System;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO
{
    public class ProductDto
    {
        public Guid? ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
