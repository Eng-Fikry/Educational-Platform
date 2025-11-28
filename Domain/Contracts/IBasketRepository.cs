using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Basket;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<Basket> GetBasketByIdAsync(string BasketId);
        Task<Basket> CreateOrUpdateBasketAsync(Basket basket, TimeSpan? TimeToLive = null);
        Task<bool> DeleteBasket(string BasketId);
    }
}
