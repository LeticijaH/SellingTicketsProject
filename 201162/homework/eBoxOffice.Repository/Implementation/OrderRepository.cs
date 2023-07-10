using eBoxOffice.Domain;
using eBoxOffice.Domain.Domain_models;
using eBoxOffice.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eBoxOffice.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = _context.Set<Order>();
        }


        public List<Order> GetAllOrders()
        {
           return entities.Include("OrderedBy")
                .Include("TicketsInOrder")
                .Include("TicketsInOrder.Ticket")
                .Include("TicketsInOrder.Ticket.Movie")
                .ToList();
        }

        public Order GetOrderDetails(BaseEntity entity)
        {
            return entities.Include("OrderedBy")
                .Include("TicketsInOrder")
                .Include("TicketsInOrder.Ticket")
                .Include("TicketsInOrder.Ticket.Movie")
                .SingleOrDefaultAsync(u => u.Id == entity.Id).Result;
        }

        public void Insert(Order entity)
        {
            if (entity == null)
            {
                throw
                    new ArgumentNullException("entity is null");
            }

            entities.Add(entity);
            _context.SaveChanges();
        }
    }
}
