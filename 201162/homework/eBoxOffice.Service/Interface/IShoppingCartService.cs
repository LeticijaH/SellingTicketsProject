using eBoxOffice.Domain.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBoxOffice.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto GetShoppingCartInfo(string userId);
        bool DeleteProductFromShoppingCart(string userId, int ticketId);
        bool OrderNow(string userId);
    }
}
