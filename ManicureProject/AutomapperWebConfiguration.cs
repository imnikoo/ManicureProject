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

                x.CreateMap<Purchase, PurchaseDTO>();
                x.CreateMap<PurchaseDTO, Purchase>();

                x.CreateMap<Item, ItemDTO>()
                  .ForMember(dest => dest.Purchases, opt => opt.MapFrom(src => Mapper.Map<IEnumerable<Purchase>, IEnumerable<PurchaseDTO>>(src.Purchases)));
                x.CreateMap<ItemDTO, Item>()
                  .ForMember(dest => dest.Purchases, opt => opt.MapFrom(src => Mapper.Map<IEnumerable<PurchaseDTO>, IEnumerable<Purchase>>(src.Purchases)));


                x.CreateMap<OrderItem, OrderItemDTO>()
                    .ForMember(dest => dest.Item, opt => opt.MapFrom(src => Mapper.Map<Item, ItemDTO>(src.Item)));
                x.CreateMap<OrderItemDTO, OrderItem>()
                    .ForMember(dest => dest.Item, opt => opt.MapFrom(src => Mapper.Map<ItemDTO, Item>(src.Item)));

                x.CreateMap<Order, OrderDTO>()
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => Mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(src.Items)));
                x.CreateMap<OrderDTO, Order>()
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => Mapper.Map<IEnumerable<OrderItemDTO>, IEnumerable<OrderItem>>(src.Items)));


                x.CreateMap<Client, ClientDTO>()
                    .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(src.Orders)));

               
            });
        }
    }
}