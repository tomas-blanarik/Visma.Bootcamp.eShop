using AutoMapper;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Domain;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;

namespace Visma.Bootcamp.eShop.ApplicationCore.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Basket, BasketDto>();
            CreateMap<BasketItem, BasketItemDto>();

            CreateMap<Catalog, CatalogDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<Product, ProductDto>();
        }
    }
}
