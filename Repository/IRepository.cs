using System.Collections.Generic;

namespace Repository
{
    public interface IRepository<T>
    {
        T Get(T item);
        T GetById(string id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Search(string query);
        void Insert(T newItem, out bool successful);
        void Update(T changedItem, out bool successful);
        void Delete(string id, out bool successful);
    }
}