using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessOrderManager.Models;
using BusinessOrderManager.Repositories;
using BusinessOrderManager.Services;
using BusinessOrderManager.Interfaces;

namespace BusinessOrderManager
{
    class Program
    {
        static void Main(string[] args)
        {
            RunDemo();
           
        }
        static void RunDemo()
        {
            Repository<Client> clientRepository = new Repository<Client>();
            Repository<Order> orderRepository = new Repository<Order>();

            IClientService clientService = new ClientService(clientRepository, orderRepository);
            IOrderService orderService = new OrderService(orderRepository, clientRepository);

            Client invalidClient = new Client
            {
                Id = 0
            };
            invalidClient.ChangeName("Тестовый клиент");

            try
            {
                clientService.AddClient(invalidClient);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }


            Client client = new Client
            {
                Id = 1,
            };
            client.ChangeName("Иван");
            client.ChangeEmail("ivan@mail.ru");
            client.ChangePhone("+7 999 123-45-67");

            Client duplicateClient = new Client
            {
                Id = 1,
            };
            duplicateClient.ChangeName("Дубликат");
            duplicateClient.ChangeEmail("duplicate@mail.ru");
            duplicateClient.ChangePhone("+7 999 111-11-11");

            Client client2 = new Client
            {
                Id = 2,
            };
            client2.ChangeName("Анна");
            client2.ChangeEmail("anna@mail.ru");
            client2.ChangePhone("+7 999 555-11-22");

            Order order = new Order
            {
                Id = 1,
            };
            order.AssignClient(1);
            order.ChangeName("Заказ №1");
            order.ChangeTotal(1500m);

            Order secondOrder = new Order
            {
                Id = 2,
            };
            secondOrder.AssignClient(2);
            secondOrder.ChangeName("Заказ №2");
            secondOrder.ChangeTotal(500m);

            Order duplicateOrder = new Order
            {
                Id = 1,
            };
            duplicateOrder.AssignClient(2);
            duplicateOrder.ChangeName("Дубликат заказа");
            duplicateOrder.ChangeTotal(500m);

            clientService.AddClient(client);

            try
            {
                clientService.AddClient(duplicateClient);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            clientService.AddClient(client2);

            try
            {
                clientService.AddClient(null);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }


            Order invalidOrder = new Order
            {
                Id = 3,
            };
            invalidOrder.AssignClient(999);
            invalidOrder.ChangeName("Тестовый заказ");
            invalidOrder.ChangeTotal(1000m);

            try
            {
                orderService.AddOrder(invalidOrder);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                orderService.AddOrder(null);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }


            orderService.AddOrder(order);
            orderService.AddOrder(secondOrder);

            decimal totalAmount = orderService.GetTotalAmount();
            Console.WriteLine($"Общая сумма заказов: {totalAmount}");

            decimal averageAmount = orderService.GetAverageOrderAmount();
            Console.WriteLine($"Средняя сумма заказа: {averageAmount}");

            decimal maxAmount = orderService.GetMaxOrderAmount();
            Console.WriteLine($"Максимальная сумма заказа: {maxAmount}");

            decimal minAmount = orderService.GetMinOrderAmount();
            Console.WriteLine($"Минимальная сумма заказа: {minAmount}");

            Order mostExpensiveOrder = orderService.GetMostExpensiveOrder();
            mostExpensiveOrder?.ShowInfo();

            Order cheapestOrder = orderService.GetCheapestOrder();
            cheapestOrder?.ShowInfo();

            try
            {
                orderService.AddOrder(duplicateOrder);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            List<Order> sortedOrders = orderService.GetOrdersSortedByTotal();
            foreach (Order item in sortedOrders)
            {
                item.ShowInfo();
            }

            List<Order> sortedOrdersDescending = orderService.GetOrdersSortedByTotalDescending();
            foreach (Order item in sortedOrdersDescending)
            {
                item.ShowInfo();
            }

            Console.WriteLine($"Количество заказов: {orderService.GetOrderCount()}");

            orderService.AssignClient(1, 2);
            List<Order> annaOrders = orderService.GetOrdersByClientId(2);
            bool annaHasOrders = orderService.HasOrders(2);
            int annaOrderCount = orderService.GetOrderCountByClientId(2);
            Console.WriteLine($"Количество заказов Анна: {annaOrderCount}");
            Order firstAnnaOrder = orderService.GetFirstOrderByClientId(2);
            firstAnnaOrder?.ShowInfo();

            bool ivanHasOrders = orderService.HasOrders(1);
            int ivanOrderCount = orderService.GetOrderCountByClientId(1);
            Order firstIvanOrder = orderService.GetFirstOrderByClientId(1);
            if (firstIvanOrder == null)
            {
                Console.WriteLine("У Ивана нет заказов");
            }
            Console.WriteLine($"Количество заказов Ивана: {ivanOrderCount}");


            Console.WriteLine($"У Ивана есть заказы: {ivanHasOrders}");
            Console.WriteLine($"У Анны есть заказы: {annaHasOrders}");
            foreach (Order item in annaOrders)
            {
                item.ShowInfo();
            }

            List<Order> ivanOrders = orderService.GetOrdersByClientId(1);
            Console.WriteLine($"Заказов Ивана: {ivanOrders.Count}");

            try
            {
                orderService.GetOrdersByClientId(999);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                orderService.AssignClient(1, 99);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                orderService.AssignClient(1, 0);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            List<Order> orders = orderService.GetAllOrders();
            foreach (Order item in orders)
            {
                item.ShowInfo();
            }
            Order foundedOrder = orderService.GetOrderById(1);
            foundedOrder.ShowInfo();
            orderService.ChangeOrderName(1, "Заказ №1 - обновлен");
            try
            {
                orderService.ChangeOrderName(1, "");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            foundedOrder.ShowInfo();
            orderService.ChangeOrderStatus(1, OrderStatus.InProgress);
            foundedOrder.ShowInfo();
            Order checkOrder = orderService.GetOrderById(1);
            checkOrder.ShowInfo();
            orderService.ChangeOrderStatus(1, OrderStatus.Cancelled);
            foundedOrder.ShowInfo();
            try
            {
                orderService.ChangeOrderStatus(1, OrderStatus.Completed);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //foundedOrder.ShowInfo();

            orderService.ChangeOrderTotal(1, 2000m);
            try
            {
                orderService.ChangeOrderTotal(1, 0);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            foundedOrder.ShowInfo();

            Client foundClient = clientService.GetClientById(1);
            foundClient.ShowInfo();
            clientService.ChangeClientEmail(1, "ivan.new@mail.ru");
            clientService.ChangeClientName(1, "Иван Петров");
            clientService.ChangeClientPhone(1, "+7 999 000-00-00");
            try
            {
                clientService.ChangeClientPhone(1, "");

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                clientService.ChangeClientEmail(1, "");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                clientService.ChangeClientName(1, "");
            }
            catch (ArgumentException ex )
            {
                Console.WriteLine(ex.Message);
            }
            foundClient.ShowInfo();
            
            Console.WriteLine($"Клиентов в репозитории: {clientService.GetClientCount()}");
            List<Client> clients = clientService.GetAllClients();
            foreach (Client item in clients)
            {
                item.ShowInfo();
            }

            try
            {
                Client missingClient = clientService.GetClientById(99);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                clientService.RemoveClient(null);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine($"Клиентов после проверки null: {clientService.GetClientCount()}");

            bool removed = false;
            try
            {
                removed = clientService.RemoveClient(client2);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            bool removedIvan = clientService.RemoveClient(client);
            Console.WriteLine($"Уален Иван: {removedIvan}");

            Console.WriteLine($"Удален клиент: {removed}");

            Console.WriteLine($"Клиентов после удаления: {clientService.GetClientCount()}");
        }
    }
}
