using eBoxOffice.Domain;
using eBoxOffice.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eBoxOffice.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly ApplicationDbContext context;
        private DbSet<T> entities;
        string errorMessage = String.Empty;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }


        public T Get(int id)
        {
            return entities.SingleOrDefault(e => e.Id == id);
        }
       
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw 
                    new ArgumentNullException("entity is null");
            }

            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity is null");
            }

            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity is null");
            }

            entities.Remove(entity);
            context.SaveChanges();
        }

    }
}
