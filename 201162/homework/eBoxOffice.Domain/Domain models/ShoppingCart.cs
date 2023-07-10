using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eBoxOffice.Domain.Domain_models
{
    public class ShoppingCart : BaseEntity
    {
        public string ApplicationUserid { get; set; }

        public ICollection<TicketsInShoppingCart> TicketsInShoppingCart { get; set; }
    }
}
