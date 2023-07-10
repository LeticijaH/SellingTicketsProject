using eBoxOffice.Domain.Domain_models;
using eBoxOffice.Domain.dto;
using eBoxOffice.Service.Interface;
using eBoxOffice.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace eBoxOffice.Service.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<TicketsInShoppingCart> _ticketsInShoppingCartRepository;
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;

        public TicketService(ITicketRepository ticketRepository, IRepository<ShoppingCart> shoppingCartRepository,
            IUserRepository userRepository, IRepository<TicketsInShoppingCart> ticketsInShoppingCartRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _ticketsInShoppingCartRepository = ticketsInShoppingCartRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public List<Ticket> GetAllTickets()
        {
            return _ticketRepository.GetAll().ToList();
        }

        public Ticket GetDetailsForTicket(int id)
        {
            return _ticketRepository.Get(id);
        }

        public void CreateNewTicket(Ticket t)
        {
            _ticketRepository.Insert(t);
        }

        public void UpdateExistingTicket(Ticket t)
        {
            _ticketRepository.Update(t);
        }

        public void DeleteTicket(int id)
        {
            var ticket = _ticketRepository.Get(id);
            _ticketRepository.Delete(ticket);
        }

        public ShoppingCartDto GetShoppingCartInfo(int id)
        {
            throw new NotImplementedException();
        }

        public bool AddToShoppingCart(AddToShoppingCartDto model, string userId)
        {
            var user = _userRepository.Get(userId);

            var userShoppingCart = user.UserShoppingCart;
            if (userShoppingCart != null)
            {
                var ticket = _ticketRepository.Get(model.SelectedTicket.Id);
                if (ticket != null)
                {
                    //check if the ticket is already in the shopping cart
                    if (userShoppingCart.TicketsInShoppingCart.Where(p => p.CartId == userShoppingCart.Id && p.TicketId == ticket.Id).Any())
                    {
                        //if it is in the cart, then just increment the quantity
                        var existing = userShoppingCart.TicketsInShoppingCart.Where(p => p.CartId == userShoppingCart.Id && p.TicketId == ticket.Id).FirstOrDefault();
                        existing.Quantity += model.Quantity;
                        _shoppingCartRepository.Update(userShoppingCart);
                    }
                    else
                    {
                        TicketsInShoppingCart itemToAdd = new TicketsInShoppingCart
                        {
                            TicketId = ticket.Id,
                            Ticket = ticket,
                            ShoppingCart = userShoppingCart,
                            Quantity = model.Quantity
                        };

                        _ticketsInShoppingCartRepository.Insert(itemToAdd);
                    }
                    return true;

                }
                return false;
            }
            return false;
        }

    }
}
