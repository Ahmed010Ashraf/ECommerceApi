using AutoMapper;
using Domain.Models.identity;
using Shared.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
