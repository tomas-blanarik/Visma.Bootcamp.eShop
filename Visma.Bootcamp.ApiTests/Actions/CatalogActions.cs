using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using Visma.Bootcamp.ApiTests.Infrastructure;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;

namespace Visma.Bootcamp.ApiTests.Actions
{
    public class CatalogActions : MainAppActionBase
    {
        public CatalogActions() : base("Catalogs")
        {
        }

        public IRestResponse<CatalogDto> CreateCatalog(CatalogDto model)
        {
            var request = CreateRequest(Method.POST, "");
            request.AddJsonBody(AddUniqueIdentifier(model));
            // using CatalogDto instead of CatalogModel because of id for update 
            var result = Execute<CatalogDto>(request);

            if (result.Data != null)
            {
                result.Data = RemoveUniqueIdentifier(result.Data);
            }

            return result;
        }

        public IRestResponse<ProductDto> AddProductToCatalog(Guid id, ProductDto model)
        {
            var request = CreateRequest(Method.POST, $"{id}/products");
            request.AddJsonBody(AddUniqueIdentifier(model));
            // using CatalogDto instead of CatalogModel because of id for update
            var result = Execute<ProductDto>(request);

            if (result.Data != null)
            {
                result.Data = RemoveUniqueIdentifier(result.Data);
            }

            return result;
        }

        public IRestResponse<CatalogDto> UpdateCatalog(CatalogDto model, Guid id)
        {
            var request = CreateRequest(Method.PUT, id.ToString());
            request.AddJsonBody(AddUniqueIdentifier(model));
            // using CatalogDto instead of CatalogModel because of id for update
            var result = Execute<CatalogDto>(request);

            if (result.Data != null)
            {
                result.Data = RemoveUniqueIdentifier(result.Data);
            }

            return result;
        }

        public IRestResponse<object> DeleteCatalog(Guid id)
        {
            var request = CreateRequest(Method.DELETE, id.ToString());
            var result = Execute<object>(request);
            return result;
        }

        public IRestResponse<CatalogDto> GetCatalog(Guid id)
        {
            var request = CreateRequest(Method.GET, $"{id}/products");
            var result = Execute<CatalogDto>(request);

            if (result.Data != null)
            {
                result.Data = RemoveUniqueIdentifier(result.Data);
            }

            return result;
        }

        public IRestResponse<List<CatalogBaseDto>> GetAllCatalogs()
        {
            var request = CreateRequest(Method.GET, $"");
            var result = Execute<List<CatalogBaseDto>>(request);

            if (result.Data != null)
            {
                // filter out catalogs from other tests
                result.Data = result.Data.Where(x => x.Name.Contains(Current.TestScenarioCorrelationId.ToString())).ToList();
                result.Data.ForEach(c => RemoveUniqueIdentifier(c));
            }

            return result;
        }
    }
}
