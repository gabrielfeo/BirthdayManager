using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IRepository<T>
    {
        T Get(T item);
        T GetById(string id);
        ICollection<T> GetAll();
        void Insert(T newItem, out bool successful);
        void Update(T changedItem, out bool successful);
        void Delete(string id, out bool successful);
    }
}