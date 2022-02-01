using System;
using System.Collections.Generic;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Domain
{
    // non-db entity
    public class Basket
    {
        public Guid? BasketId { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}
