using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessOrderManager.Interfaces;

namespace BusinessOrderManager.Models
{
    public abstract class Entity : IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; protected set; }

        public abstract void ShowInfo();

        public void ChangeName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Название не может быть пустым.", nameof(newName));
            Name = newName;
        }
    }
}
