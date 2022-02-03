using System;
using System.Collections.Generic;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO
{
    public class BasketDto : ICacheableDto
    {
        public Guid BasketId { get; set; }
        public IList<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        public Guid Id => BasketId;
    }
}
