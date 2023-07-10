
using eBoxOffice.Domain.Domain_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBoxOffice.Domain.dto
{
    public class AddToShoppingCartDto
    {
        public Ticket SelectedTicket { get; set; }
        public int Quantity { get; set; }
    }
}
