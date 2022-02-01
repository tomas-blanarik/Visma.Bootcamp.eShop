using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;

namespace Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CatalogDto>> GetAllAsync(CancellationToken ct = default);
        Task<CatalogDto> GetAsync(Guid catalogId, CancellationToken ct = default);

        Task<CatalogDto> CreateAsync(CatalogModel model, CancellationToken ct = default);
        Task<CatalogDto> UpdateAsync(Guid catalogId, CatalogModel model, CancellationToken ct = default);
        Task DeleteAsync(Guid catalogId, CancellationToken ct = default);
    }
}
