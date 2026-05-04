using AutoMapper;
using Domain.Models.Order;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.Identity;
using Shared.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderAddress, AddressDto>().ReverseMap();

            CreateMap<Order, OrderResponse>().ForMember(dest => dest.DelivaryMethod,
                opt => opt.MapFrom(src => src.DelivaryMethod.ShortName))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Subtotal + src.DelivaryMethod.Price));

            CreateMap<OrderItems, OrderItemDto>().ForMember(dest => dest.PictureUrl,
                opt => opt.MapFrom<OrderResolver>());


            CreateMap<DelivaryMethod, DeliveryMethodResponse>();
        }

       
    }
    public class OrderResolver(IConfiguration _conifg) : IValueResolver<OrderItems, OrderItemDto, string>
    {
        public string Resolve(OrderItems source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(source.PictureUrl))
            {
                return   $"{_conifg["BaseUrl"]}{source.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
