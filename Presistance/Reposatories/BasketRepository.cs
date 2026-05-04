using Domain.Contracts;
using Domain.Models.basket;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistance.Reposatories
{
    public class BasketRepository(IConnectionMultiplexer _connection) : IBasketRepository
    {
        private readonly IDatabase _database = _connection.GetDatabase();   
        public async Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket basket , TimeSpan? timetolive = null)
        {
            var isaddedorupdated =await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), timetolive ?? TimeSpan.FromDays(7));
            if (!isaddedorupdated) return null;
            return await GetBasketAsync(basket.Id);
        }

        public Task<bool> DeleteBasketAsync(string basketId)
        {
            var isdeleted = _database.KeyDeleteAsync(basketId);
            return isdeleted;
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);
            if (basket.IsNullOrEmpty) return null;
            return  JsonSerializer.Deserialize<CustomerBasket>(basket);
        }
    }
}
