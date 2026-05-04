using AutoMapper;
using Domain.Contracts;
using Domain.Models.identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ServicesAbstraction;
using Shared.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class ServiceManager(IUniteOfWork _uniteofwork , IMapper _mapper , IBasketRepository _basketrepo
                                ,UserManager<ApplicationUser> _usermanager,
                                IOptions<JWTOptions> jwtOptions 
                              ) : IServiceManager
    {
        private readonly Lazy<IProductServices> _ProductServices = new(() => new ProductService(_uniteofwork, _mapper));
        public IProductServices IProductServices => _ProductServices.Value;


        private readonly Lazy<IBasketService> _BasketServices = new(() => new BasketService(_basketrepo, _mapper));
        public IBasketService IBasketService => _BasketServices.Value;


        private readonly Lazy<IAuthenticationService> _AuthServices = new(() => new AuthenticationService(_usermanager, jwtOptions , _mapper));
        public IAuthenticationService IAuthenticationService => _AuthServices.Value;


        private readonly Lazy<IOrderService> _OrderServices = new(() => new OrderService(  _uniteofwork,_mapper , _basketrepo));
        public IOrderService IOrderService => _OrderServices.Value;
    }
}
