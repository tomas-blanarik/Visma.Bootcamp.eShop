using System.Collections.Generic;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO
{
    public class CatalogDto : CatalogBaseDto
    {
        public List<ProductDto> Products { get; set; }
    }
}
