using System;
using System.Collections.Generic;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO
{
    public class CatalogDto : ICacheableDto
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductDto> Products { get; set; }
        public Guid Id => PublicId;
    }
}
