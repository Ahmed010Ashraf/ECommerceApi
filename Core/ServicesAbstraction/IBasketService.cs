using Shared.DTOs.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetBasketAsync(string basketId);
        Task<CustomerBasketDto> CreateOrUpdateBasketAsync(CustomerBasketDto basket, TimeSpan? timetolive = null);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
