using System;
using System.Threading;
using System.Threading.Tasks;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.eShop.ApplicationCore.Services
{
    public class ProductService : IProductService
    {
        public Task<ProductDto> CreateAsync(Guid catalogId, ProductModel model, CancellationToken ct = default)
        {
            throw new NotImplementedException("Not implemented");
        }

        public Task DeleteAsync(Guid productId, CancellationToken ct = default)
        {
            throw new NotImplementedException("Not implemented");
        }

        public async Task<ProductDto> GetAsync(Guid productId, CancellationToken ct = default)
        {
            return new ProductDto
            {
                PublicId = Guid.NewGuid(),
                Name = "product #1",
                Description = "product description #2",
                Price = 123.99M
            };
        }

        public Task<ProductDto> UpdateAsync(Guid productId, ProductModel model, CancellationToken ct = default)
        {
            throw new NotImplementedException("Not implemented");
        }
    }
}
