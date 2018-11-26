using System;
using System.Collections.Generic;
using Repository.ValidatorNs;

namespace Repository
{
    public interface IRepository<T>
    {
        T Get(string id);
        ICollection<T> GetAll();
        void Create(T newItem, out bool successful);
        void Update(T changedItem, out bool successful);
        void Delete(string id, out bool successful);
    }
}