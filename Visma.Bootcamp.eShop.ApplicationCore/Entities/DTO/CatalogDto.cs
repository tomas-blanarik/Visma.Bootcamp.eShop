using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO
{
    public class CatalogDto : ICacheableDto
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ProductDto> Products { get; set; }

        [JsonIgnore]
        public Guid Id => PublicId;
    }
}
