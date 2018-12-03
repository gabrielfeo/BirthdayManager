using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Repository.ValidatorNs;

namespace Repository
{
    public class RepositoryFactory
    {

        public IRepository<Person> NewPersonRepository()
        {
            var validator = new PersonValidator();
            return new MemoryPersonRepository(validator);
        }

    }
}