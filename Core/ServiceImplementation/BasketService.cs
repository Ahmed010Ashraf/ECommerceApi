using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.basket;
using ServicesAbstraction;
using Shared.DTOs.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class BasketService(IBasketRepository _basketrepo , IMapper _mpper) : IBasketService
    {
        public async Task<CustomerBasketDto> CreateOrUpdateBasketAsync(CustomerBasketDto basket, TimeSpan? timetolive = null)
        {
            var baskethere =  _mpper.Map<CustomerBasket>(basket);
            var createdorupdatedbasket = await _basketrepo.CreateOrUpdateBasketAsync(baskethere, timetolive) ?? throw new Exception("problem in create or update basket");

            var bas = _mpper.Map<CustomerBasketDto>(createdorupdatedbasket);
            return bas;
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            var res = await _basketrepo.DeleteBasketAsync(basketId);
            return res;
        }

        public async Task<CustomerBasketDto> GetBasketAsync(string basketId)
        {
            var basket = await  _basketrepo.GetBasketAsync(basketId) ??
        throw new BasketNotFoundException(basketId);

            return _mpper.Map<CustomerBasketDto>(basket);
        }
    }
}
