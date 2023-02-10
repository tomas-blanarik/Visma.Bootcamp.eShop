using System;
using System.Text.Json.Serialization;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO
{
    public class ProductDto : ICacheableDto
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public Guid Id => PublicId;
    }
}
