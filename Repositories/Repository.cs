using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessOrderManager.Interfaces;

namespace BusinessOrderManager.Repositories
{
    public class Repository<T> where T : IIdentifiable
    {
        private List<T> _items = new List<T>();

        public int Count
        {
            get { return _items.Count; }
        }

        public void Add(T item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException(nameof(item));

            if (item.Id <= 0)
                throw new ArgumentOutOfRangeException(nameof(item), "Id должен быть больше нуля.");

            foreach (T existingItem in _items)
            {
                if (existingItem.Id == item.Id)
                    throw new InvalidOperationException($"Объект с Id = {item.Id} уже существует.");
            }
            _items.Add(item);
        }

        public bool Remove(T item)
        {
            return _items.Remove(item);
        }

        public T GetById(int id)
        {
            foreach (T item in _items)
            {
                if (item.Id == id)
                    return item;
            }

            throw new KeyNotFoundException($"Объект с Id = {id} не найден.") ;
        }

        public List<T> GetAll()
        {
            return new List<T>(_items);
        }
    }
}
