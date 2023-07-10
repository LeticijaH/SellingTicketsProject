using eBoxOffice.Domain.identity;
using eBoxOffice.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eBoxOffice.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<CinemaUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = _context.Set<CinemaUser>();
        }

        public IEnumerable<CinemaUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public CinemaUser Get(string id)
        {
            return entities.Include("UserShoppingCart")
                .Include("UserShoppingCart.TicketsInShoppingCart")
                .Include("UserShoppingCart.TicketsInShoppingCart.Ticket")
                .Include("UserShoppingCart.TicketsInShoppingCart.Ticket.Movie")
                .SingleOrDefault(u => u.Id == id);
        }

        public void Insert(CinemaUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity is null");
            }

            entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(CinemaUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity is null");
            }

            entities.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(CinemaUser entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity is null");
            }

            entities.Remove(entity);
            _context.SaveChanges();
        }
    }
}
