using eBoxOffice.Domain;
using eBoxOffice.Domain.Domain_models;
using eBoxOffice.Repository.Interface;
using eBoxOffice.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBoxOffice.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        public Order GetOrderDetails(BaseEntity entity)
        {
            return _orderRepository.GetOrderDetails(entity);
        }
    }
}
