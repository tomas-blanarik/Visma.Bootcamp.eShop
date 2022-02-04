using System;
using System.Collections.Generic;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Domain
{
    // non-db entity
    // nosql - mongodb (document database), redis (key-value)
    public class Basket
    {
        public Guid? BasketId { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}
