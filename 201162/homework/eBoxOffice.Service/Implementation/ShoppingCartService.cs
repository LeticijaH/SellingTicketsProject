using eBoxOffice.Domain.Domain_models;
using eBoxOffice.Domain.dto;
using eBoxOffice.Repository.Interface;
using eBoxOffice.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eBoxOffice.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<TicketsInOrder> _ticketInOrderRepository;
        private readonly IRepository<EmailMessage> _mailRepository;

        public ShoppingCartService(IUserRepository userRepository, IOrderRepository orderRepository, IRepository<ShoppingCart> shoppingCartRepository, IRepository<TicketsInOrder> ticketInOrderRepository, IRepository<EmailMessage> mailRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _ticketInOrderRepository = ticketInOrderRepository;
            _mailRepository = mailRepository;
        }

        public bool DeleteProductFromShoppingCart(string userId, int ticketId)
        {
            if (string.IsNullOrEmpty(userId) || ticketId == null)
            {
                return false;
            }

            var loggedInUser = _userRepository.Get(userId);
            var userShoppingCart = loggedInUser.UserShoppingCart;
            var itemToDelete = userShoppingCart.TicketsInShoppingCart.Where(t => t.TicketId == ticketId).FirstOrDefault();

            userShoppingCart.TicketsInShoppingCart.Remove(itemToDelete);
            _shoppingCartRepository.Update(userShoppingCart);

            return true;
        }

        public ShoppingCartDto GetShoppingCartInfo(string userId)
        {
            var user = _userRepository.Get(userId);

            var userShoppingCart = user.UserShoppingCart;

            var ticketsList = userShoppingCart.TicketsInShoppingCart
                .Select(t => new
                {
                    Quantity = t.Quantity,
                    TicketPrice = t.Ticket.TicketPrice
                });

            int totalPrice = 0;
            foreach (var item in ticketsList)
            {
                totalPrice += item.TicketPrice * item.Quantity;
            }

            ShoppingCartDto model = new ShoppingCartDto
            {
                TotalPrice = totalPrice,
                TicketsInShoppingCarts = userShoppingCart.TicketsInShoppingCart.ToList()
            };

            return model;
        }

        public bool OrderNow(string userId)
        {
            var user = _userRepository.Get(userId);

            var userShoppingCart = user.UserShoppingCart;

            Order newOrder = new Order
            {
                UserId = user.Id,
                OrderedBy = user
            };

            _orderRepository.Insert(newOrder);

            List<TicketsInOrder> ticketsInOrders = userShoppingCart.TicketsInShoppingCart
                .Select(t => new TicketsInOrder
                {
                    Ticket = t.Ticket,
                    TicketId = t.TicketId,
                    Order = newOrder,
                    OrderId = newOrder.Id,
                    Quantity = t.Quantity
                }).ToList();

            foreach (var item in ticketsInOrders)
            {
                _ticketInOrderRepository.Insert(item);
            }

            user.UserShoppingCart.TicketsInShoppingCart.Clear();
            _userRepository.Update(user);

            EmailMessage message = new EmailMessage();
            message.MailTo = user.Email;
            message.Subject = "Successfully created order";
            message.Status = false;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Your order is completed. The order contains: ");
            int totalPrice = 0;

            for(int i = 1; i <= ticketsInOrders.Count(); i++)
            {
                var item = ticketsInOrders[i - 1];
                var price = item.Ticket.TicketPrice * item.Quantity;
                totalPrice += price;
                sb.AppendLine(i.ToString() + ". " + item.Ticket.Movie.MovieName + " " + item.Quantity + " x $" + item.Ticket.TicketPrice + " = $" + price);
            }

            sb.AppendLine("Total price: $" + totalPrice);
            
            message.Body = sb.ToString();

            _mailRepository.Insert(message);

            return true;
        }
    }
}
