using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessOrderManager.Models;
using BusinessOrderManager.Repositories;
using BusinessOrderManager.Interfaces;

namespace BusinessOrderManager.Services
{
    public class OrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Client> _clientRepository;
        public OrderService(IRepository<Order> orderRepository, IRepository<Client> clientRepository)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
        }

        public void AddOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _clientRepository.GetById(order.ClientId);
            _orderRepository.Add(order);
        }

        public Order GetOrderById(int id)
        {
            return _orderRepository.GetById(id);
        }

        public List<Order> GetOrdersByClientId(int clientId)
        {
            _clientRepository.GetById(clientId);

            return GetAllOrdersInternal()
                .Where(order => order.ClientId == clientId)
                .ToList();
        }

        public void ChangeOrderStatus(int orderId, OrderStatus newStatus)
        {
            Order order = _orderRepository.GetById(orderId);
            order.ChangeStatus(newStatus);
        }

        public void ChangeOrderTotal(int orderId, decimal newTotal)
        {
            Order order = _orderRepository.GetById(orderId);
            order.ChangeTotal(newTotal);
        }

        public void ChangeOrderName(int orderId, string newName)
        {
            Order order = _orderRepository.GetById(orderId);
            order.ChangeName(newName);
        }

        public void AssignClient(int orderId, int clientId)
        {
            if (clientId <= 0)
                throw new ArgumentOutOfRangeException(nameof(clientId), "Id клиента должен быть больше нуля.");
            Client client = _clientRepository.GetById(clientId);
            Order order = _orderRepository.GetById(orderId);
            order.AssignClient(client.Id);
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        public int GetOrderCount()
        {
            return _orderRepository.Count;
        }

        public bool HasOrders(int clientId)
        {
            return GetAllOrdersInternal()
                .Any(order => order.ClientId == clientId);
        }

        public int GetOrderCountByClientId(int clientId)
        {
            return GetAllOrdersInternal()
                .Count(order => order.ClientId == clientId);
        }

        public Order GetFirstOrderByClientId(int clientId)
        {
            return GetAllOrdersInternal()
                .FirstOrDefault(order => order.ClientId == clientId);
        }

        public List<Order> GetOrdersSortedByTotal()
        {
            return GetAllOrdersInternal()
                .OrderBy(order => order.Total)
                .ToList();
        }

        public List<Order> GetOrdersSortedByTotalDescending()
        {
            return GetAllOrdersInternal()
                .OrderByDescending(order => order.Total)
                .ToList();
        }

        public decimal GetTotalAmount()
        {
            return GetAllOrdersInternal()
                .Sum(order => order.Total);
        }

        public decimal GetAverageOrderAmount()
        {
            return GetAllOrdersInternal()
                .Average(order => order.Total);
        }

        public decimal GetMaxOrderAmount()
        {
            return GetAllOrdersInternal()
                .Max(order => order.Total);
        }

        public decimal GetMinOrderAmount()
        {
            return GetAllOrdersInternal()
                .Min(order => order.Total);
        }

        public Order GetMostExpensiveOrder()
        {
            return GetAllOrdersInternal()
                .OrderByDescending(order => order.Total)
                .FirstOrDefault();
        }

        public Order GetCheapestOrder()
        {
            return GetAllOrdersInternal()
                .OrderBy(order => order.Total)
                .FirstOrDefault();
        }

        private List<Order> GetAllOrdersInternal()
        {
            return _orderRepository.GetAll();
        }
    }
}
