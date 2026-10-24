using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessOrderManager.Models;
using BusinessOrderManager.Repositories;

namespace BusinessOrderManager.Services
{
    public class ClientService
    {
        private readonly Repository<Client> _clientRepository;
        private readonly Repository<Order> _orderRepository;
        public ClientService(Repository<Client> clientRepository, Repository<Order> orderRepository)
        {
            _clientRepository = clientRepository;
            _orderRepository = orderRepository;
        }

        public void AddClient(Client client)
        {
            _clientRepository.Add(client);
        }

        public bool RemoveClient(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (_orderRepository
                .GetAll()
                .Any(order => order.ClientId == client.Id))
            {
                throw new InvalidOperationException("Нельзя удалить клиента, у которого есть заказы.");
            }

            return _clientRepository.Remove(client);
        }

        public Client GetClientById(int id)
        {
            return _clientRepository.GetById(id);
        }

        public List<Client> GetAllClients()
        {
            return _clientRepository.GetAll();
        }

        public int GetClientCount()
        {
            return _clientRepository.Count;
        }

        public void ChangeClientEmail(int clientId, string newEmail)
        {
            Client client = _clientRepository.GetById(clientId);
            client.ChangeEmail(newEmail);
        }

        public void ChangeClientPhone(int clientId, string newPhone)
        {
            Client client = _clientRepository.GetById(clientId);
            client.ChangePhone(newPhone);
        }

        public void ChangeClientName(int clientId, string newName)
        {
            Client client = _clientRepository.GetById(clientId);
            client.ChangeName(newName);
        }
    }
}
