using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.BrandName,
                options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.TypeName, options => options
                .MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, options =>
                options.MapFrom<ProfileUrlResolver>());


            CreateMap<ProductBrand, BrandResponse>();
            CreateMap<ProductType, TypeResponse>();
        }

        public class ProfileUrlResolver(IConfiguration _config) : IValueResolver<Product, ProductResponse, string>
        {
            public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
            {
                if (!string.IsNullOrWhiteSpace(source.PictureUrl))
                {
                    return $"{_config["BaseUrl"]}{source.PictureUrl}";
                }
                return string.Empty;
            }
        }
    }
}
