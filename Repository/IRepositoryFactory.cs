using System;

namespace Repository
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepositoryOf<T>(T type);
    }
}