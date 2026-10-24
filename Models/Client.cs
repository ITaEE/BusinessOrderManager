using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessOrderManager.Models
{
    public class Client : Entity
    {
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public override void ShowInfo()
        {
            Console.WriteLine($"{Id}: {Name}, Email: {Email}, Phone: {Phone}");
        }

        public void ChangeEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("Eamil не может быть пустым.", nameof(newEmail));

            Email = newEmail;
        }

        public void ChangePhone(string newPhone)
        {
            if (string.IsNullOrWhiteSpace(newPhone))
                throw new ArgumentException("Телефон не может быть пустым.", nameof(newPhone));
            Phone = newPhone;
        }

    }
}
