using System;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;

namespace Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces
{
    public interface IBasketService
    {
        BasketDto AddItem(Guid basketId, BasketItemModel model);
        BasketDto Get(Guid basketId);
        void Update(Guid basketId, BasketModel model);
        void DeleteItem(Guid basketId, Guid itemId);
    }
}
