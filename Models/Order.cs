using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessOrderManager.Models
{
    public class Order : Entity
    {
        public decimal Total { get; private set; }
        public int ClientId { get; private set; }
        public OrderStatus Status { get; private set; }
        public Order()
        {
            Status = OrderStatus.New;
        }
        public override void ShowInfo()
        {
            Console.WriteLine($"{Id}: {Name}, ClientId: {ClientId}, Status: {Status}, Total: {Total}");
        }

        public void ChangeStatus(OrderStatus newStatus)
        {
            if (Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("Нельзя изменить статус отмененного заказа.");
            Status = newStatus;
        }

        public void ChangeTotal(decimal newTotal)
        {
            if (newTotal <= 0)
                throw new ArgumentOutOfRangeException(nameof(newTotal), "Сумма заказа должна быть больше нуля.");
            Total = newTotal;
        }

        public void AssignClient(int clientId)
        {
            if (clientId <= 0)
                throw new ArgumentOutOfRangeException(nameof(clientId), "Id клиента должен быть больше нуля.");
            ClientId = clientId;
        }

    }
}
