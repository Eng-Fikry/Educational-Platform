using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.Basket;
using StackExchange.Redis;

namespace Presistance.Reposatories
{
    public class BasketRepository(IConnectionMultiplexer _connection,IMapper _mapper) : IBasketRepository
    {
        private readonly IDatabase _database = _connection.GetDatabase();
        public async Task<Basket> CreateOrUpdateBasketAsync(Basket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromDays(30));
            if (IsCreatedOrUpdated)
            {
                return await GetBasketByIdAsync(basket.Id);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteBasket(string BasketId) => await _database.KeyDeleteAsync(BasketId);

        public async Task<Basket> GetBasketByIdAsync(string BasketId)
        {
            var Basket = await _database.StringGetAsync(BasketId);
            if(Basket.IsNullOrEmpty)
            {
                return null!;
            }
            return JsonSerializer.Deserialize<Basket>(Basket!)!;
        }
    }
}
