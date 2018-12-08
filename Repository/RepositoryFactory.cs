using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Repository.ValidatorNs;

namespace Repository
{
    public class RepositoryFactory
    {
        public IRepository<TData> NewRepository<TData>()
        {
            if (typeof(TData) == typeof(Person))
            {
                var validator = new PersonValidator();
                return new MemoryPersonRepository(validator) as IRepository<TData>;
            }

            else
                throw new ArgumentException($"There is no Repository available for {typeof(TData)}");
        }
    }
}