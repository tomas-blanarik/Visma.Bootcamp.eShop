using System;
using System.Collections.Generic;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;

namespace Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces
{
    public interface ICatalogService
    {
        List<CatalogDto> Get();
        CatalogDto Get(Guid catalogId);
        CatalogDto Create(CatalogModel model);
        CatalogDto Update(Guid catalogId, CatalogModel model);
        void Delete(Guid catalogId);
    }
}
