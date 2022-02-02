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

            CreateMap<Catalog, CatalogDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<Product, ProductDto>();

            CreateMap<CatalogModel, CatalogDto>();
            CreateMap<ProductModel, ProductDto>();
            CreateMap<BasketModel, BasketDto>();
            CreateMap<BasketItemModel, BasketItemDto>()
                .ForMember(dest => dest.Product, src => src.MapFrom(x => new ProductDto { PublicId = x.ProductId }));
        }
    }
}
