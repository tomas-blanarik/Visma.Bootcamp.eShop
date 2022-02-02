using System;
using System.Collections.Generic;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Domain
{
    // non-db entity
    internal class Basket : IDomain
    {
        public Guid BasketId { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}
