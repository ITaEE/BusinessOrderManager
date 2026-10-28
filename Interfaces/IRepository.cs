using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessOrderManager.Interfaces
{
    public interface IRepository<T> where T : IIdentifiable
    {
        void Add(T item);
        T GetById(int id);
        List<T> GetAll();
        int Count { get; }
        bool Remove(T item);
    }
}
