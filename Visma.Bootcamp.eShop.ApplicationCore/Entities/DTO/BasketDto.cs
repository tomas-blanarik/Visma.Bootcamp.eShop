using System;
using System.Collections.Generic;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO
{
    public class BasketDto
    {
        public Guid? BasketId { get; set; }
        public IList<BasketItemDto> Items { get; set; }
    }
}
