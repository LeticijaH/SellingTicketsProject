using eBoxOffice.Domain.Domain_models;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBoxOffice.Repository.Interface
{
    public interface ITicketRepository
    {
        List<Ticket> GetAll();
        Ticket Get(int id);
        void Insert(Ticket entity);
        void Update(Ticket entity);
        void Delete(Ticket entity);
    }
}
