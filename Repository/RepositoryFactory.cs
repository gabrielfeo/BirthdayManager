using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Repository.PersonNs;
using Validator;

namespace Repository
{
    public class RepositoryFactory
    {
        public enum StorageOption
        {
            Memory, Filesystem, RelationalDatabase
        }


        public IRepository<TData> NewRepository<TData>(StorageOption storage, IValidator<TData> validator)
        {
            return Resolve(storage, validator);
        }

        private IRepository<TData> Resolve<TData>(StorageOption storage, IValidator<TData> validator)
        {
            if (typeof(TData) == typeof(Person))
            {
                var personValidator = validator as IValidator<Person>;
                switch (storage)
                {
                    case StorageOption.Memory:
                        return new MemoryPersonRepository(personValidator) as IRepository<TData>;
                    case StorageOption.Filesystem:
                        return new FilesystemPersonRepository(personValidator) as IRepository<TData>;
                    default:
                        throw new ArgumentException($"No implementation available for storage option {storage}",
                                                    nameof(storage));
                }
            }
            throw new ArgumentException($"There is no Repository available for {typeof(TData)}",
                                        nameof(TData));
        }
    }
}