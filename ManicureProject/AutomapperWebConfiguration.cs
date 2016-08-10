using AutoMapper;
using ManicureDomain.DTOs;
using ManicureDomain.Entities;
using System.Collections.Generic;

namespace ManicureProject
{
    public static class AutomapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<Category, CategoryDTO>();
                x.CreateMap<CategoryDTO, Category>();

                x.CreateMap<City, CityDTO>();
                x.CreateMap<CityDTO, City>();

                x.CreateMap<PurchasePlace, PurchasePlaceDTO>();
                x.CreateMap<PurchasePlaceDTO, PurchasePlace>();

                x.CreateMap<OrderItem, OrderItemDTO>();
                x.CreateMap<OrderItemDTO, OrderItem>();

                x.CreateMap<Purchase, PurchaseDTO>();
                x.CreateMap<PurchaseDTO, Purchase>();

                x.CreateMap<Order, OrderDTO>()
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => Mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(src.Items)));
                x.CreateMap<OrderDTO, Order>();

                x.CreateMap<Client, ClientDTO>()
                    .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(src.Orders)));

                x.CreateMap<Item, ItemDTO>()
                   .ForMember(dest => dest.Purchases, opt => opt.MapFrom(src => Mapper.Map<IEnumerable<Purchase>, IEnumerable<PurchaseDTO>>(src.Purchases)));

            });
        }
    }
}