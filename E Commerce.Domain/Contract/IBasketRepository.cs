using E_Commerce.Domain.Entities.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contract
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId);
        Task<CustomerBasket?> CreateIrUpdateBasketAsync (CustomerBasket basket,TimeSpan TimeToLive= default);

        Task<bool> DeleteBasketAsync (string basketId);
    }
}
