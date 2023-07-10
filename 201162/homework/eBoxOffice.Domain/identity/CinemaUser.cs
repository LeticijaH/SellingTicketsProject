
using eBoxOffice.Domain.Domain_models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBoxOffice.Domain.identity
{
    public class CinemaUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public virtual ShoppingCart UserShoppingCart { get; set; }
    }
}
