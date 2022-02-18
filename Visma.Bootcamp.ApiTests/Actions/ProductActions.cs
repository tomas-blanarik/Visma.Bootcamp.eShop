using System;
using RestSharp;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;

namespace Visma.Bootcamp.ApiTests.Actions
{
    public class ProductActions : MainAppActionBase
    {
        public ProductActions() : base("Products")
        {
        }

        public IRestResponse<ProductDto> GetProduct(Guid id)
        {
            var request = CreateRequest(Method.GET, $"{id}");
            var result = Execute<ProductDto>(request);

            if (result.Data != null)
            {
                result.Data = RemoveUniqueIdentifier(result.Data);
            }

            return result;
        }

        public IRestResponse<ProductDto> UpdateProduct(ProductDto model, Guid id)
        {
            var request = CreateRequest(Method.PUT, id.ToString());
            request.AddJsonBody(AddUniqueIdentifier(model));
            // using ProductDto instead of ProductModel because of id for update
            var result = Execute<ProductDto>(request);

            if (result.Data != null)
            {
                result.Data = RemoveUniqueIdentifier(result.Data);
            }

            return result;
        }
    }
}
