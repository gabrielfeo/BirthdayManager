using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IRepository<T>
    {
        T Get(Guid id);
        ICollection<T> GetAll();
        void Create(T newItem, out bool successful);
        void Update(T changedItem, out bool successful);
        void Delete(Guid id, out bool successful);
    }
}