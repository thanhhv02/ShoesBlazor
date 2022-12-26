using Blazored.LocalStorage;
using PoPoy.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Client.State
{
    public interface ICartState
    {
        ValueTask UpdateAsync();
        event Action<int> OnQuantityChanged;
    }

    public class CartState : ICartState
    {
        private readonly ILocalStorageService storageService;

        public event Action<int> OnQuantityChanged;

        public CartState(ILocalStorageService storageService)
            => this.storageService = storageService;

        public async ValueTask UpdateAsync()
        {
            var carts = await storageService.GetItemAsync<List<CartStorage>>("cart");
            int count = carts is null
                ? 0
                : carts.Sum(x => x.Quantity);
            OnQuantityChanged?.Invoke(count);
        }
    }
}



