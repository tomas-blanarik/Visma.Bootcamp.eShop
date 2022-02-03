using AutoMapper;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Domain;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;

namespace Visma.Bootcamp.eShop.ApplicationCore.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Basket, BasketDto>();
            CreateMap<BasketItem, BasketItemDto>();

            CreateMap<Catalog, CatalogDto>()
                .ForMember(dest => dest.CatalogId, src => src.MapFrom(x => x.CatalogId.Value));
            CreateMap<Order, OrderDto>();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductId, src => src.MapFrom(x => x.ProductId.Value));

            CreateMap<CatalogModel, CatalogDto>();
        }
    }
}
