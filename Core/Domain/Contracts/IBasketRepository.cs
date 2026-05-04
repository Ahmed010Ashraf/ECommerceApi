using Domain.Models.basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string basketId);
        Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket basket ,TimeSpan? timetolive=null);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
