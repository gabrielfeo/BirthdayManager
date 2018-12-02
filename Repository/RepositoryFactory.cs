using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Repository.ValidatorNs;

namespace Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {

        public IRepository<T> GetRepositoryOf<T>(T type) => ConstructRepositoryFor(type);

        private IRepository<T> ConstructRepositoryFor<T>(T type)
        {
            if (type is Person person)
            {
                return (IRepository<T>) ConstructPersonRepository();
            }
            else 
            {
                throw new ArgumentException($"No Repository available for {type}");
            }
        }

        private IRepository<Person> ConstructPersonRepository() 
        {
            var validator = new PersonValidator();
            return new MemoryPersonRepository(validator);
        }

    }
}