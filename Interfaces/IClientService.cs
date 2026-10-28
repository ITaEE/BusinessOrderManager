using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessOrderManager.Models;

namespace BusinessOrderManager.Interfaces
{
    public interface IClientService
    {
        void AddClient(Client client);
        Client GetClientById(int id);
        List<Client> GetAllClients();
        bool RemoveClient(Client client);
        void ChangeClientEmail(int clientId, string newEmail);
        void ChangeClientPhone(int clientId, string newPhone);
        void ChangeClientName(int clientId, string newName);
        int GetClientCount();
    }
}
