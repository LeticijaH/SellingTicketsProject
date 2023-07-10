using eBoxOffice.Domain;
using eBoxOffice.Domain.Domain_models;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBoxOffice.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        Order GetOrderDetails(BaseEntity entity);
        void Insert(Order entity);
    }
}
