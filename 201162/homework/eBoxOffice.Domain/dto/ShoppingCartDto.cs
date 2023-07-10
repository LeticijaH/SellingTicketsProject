using eBoxOffice.Domain.Domain_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBoxOffice.Domain.dto
{
    public class ShoppingCartDto
    {
        public List<TicketsInShoppingCart> TicketsInShoppingCarts { get; set; }
        public int TotalPrice { get; set; }
    }
}
