using eBoxOffice.Domain.Domain_models;
using eBoxOffice.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eBoxOffice.Repository.Implementation
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Ticket> entities;
        string errorMessage = string.Empty;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = _context.Set<Ticket>();
        }


        public List<Ticket> GetAll()
        {
            return entities.Include(t => t.Movie).ToList();
        }


        public Ticket Get(int id)
        {
            return entities.Include(t => t.Movie)
                .SingleOrDefault(t => t.Id == id);
        }

        public void Insert(Ticket entity)
        {
            if (entity == null)
            {
                throw
                    new ArgumentNullException("entity is null");
            }

            entities.Add(entity);
            _context.SaveChanges();
        }


        public void Update(Ticket entity)
        {
            if (entity == null)
            {
                throw
                    new ArgumentNullException("entity is null");
            }

            entities.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(Ticket entity)
        {
            if (entity == null)
            {
                throw
                    new ArgumentNullException("entity is null");
            }

            entities.Remove(entity);
            _context.SaveChanges();
        }
    }
}
