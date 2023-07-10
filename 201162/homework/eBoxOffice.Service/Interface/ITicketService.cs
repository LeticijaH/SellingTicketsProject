using eBoxOffice.Domain.Domain_models;
using eBoxOffice.Domain.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBoxOffice.Service.Interface
{
    public interface ITicketService
    {

        List<Ticket> GetAllTickets();
        Ticket GetDetailsForTicket(int id);
        void CreateNewTicket(Ticket t);
        void UpdateExistingTicket(Ticket t);
        ShoppingCartDto GetShoppingCartInfo(int id);
        void DeleteTicket(int id);
        bool AddToShoppingCart(AddToShoppingCartDto model, string userId);
    }
}
