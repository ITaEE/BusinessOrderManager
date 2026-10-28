using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessOrderManager.Models;

namespace BusinessOrderManager.Interfaces
{
    public interface IOrderService
    {
        void AddOrder(Order order);
        Order GetOrderById(int id);
        List<Order> GetAllOrders();
        void ChangeOrderStatus(int orderId, OrderStatus newStatus);
        void ChangeOrderTotal(int orderId, decimal newTotal);
        void ChangeOrderName(int orderId, string newName);
        void AssignClient(int orderId, int clientId);
        List<Order> GetOrdersByClientId(int clientId);
        bool HasOrders(int clientId);
        int GetOrderCount();
        int GetOrderCountByClientId(int clientId);
        decimal GetTotalAmount();
        decimal GetAverageOrderAmount();
        decimal GetMaxOrderAmount();
        decimal GetMinOrderAmount();
        Order GetMostExpensiveOrder();
        Order GetFirstOrderByClientId(int clientId);
        List<Order> GetOrdersSortedByTotal();
        List<Order> GetOrdersSortedByTotalDescending();
        Order GetCheapestOrder();
    }
}
