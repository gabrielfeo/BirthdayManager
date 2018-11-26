using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Repository.ValidatorNs;

namespace Repository
{
    internal abstract class PersonRepository : IRepository<Person>
    {

        protected IValidator<Person> _validator;

        protected PersonRepository(IValidator<Person> personValidator)
        {
            _validator = personValidator;
        }

        public abstract ICollection<Person> GetAll();
        public abstract Person Get(string personId);

        public virtual void Create(Person newPerson, out bool successful)
        {
            successful = false;
            var isValidPerson = _validator.Validate(newPerson);
            var isImpostor = IsKnownPerson(newPerson);
            if (!isValidPerson || isImpostor) return;
        }
        public virtual void Update(Person changedPerson, out bool successful)
        {
            successful = false;
            var isValidPerson = _validator.Validate(changedPerson);
            var isKnown = IsKnownPerson(changedPerson);
            if (!isValidPerson || !isKnown) return;
        }
        public virtual void Delete(string personId, out bool successful)
        {
            successful = false;
            var isKnown = IsKnownPersonId(personId);
            if (!isKnown) return;
        }

        private bool IsKnownPerson(Person person)
        {
            return GetAll().AsEnumerable().Contains(person);
        }

        private bool IsKnownPersonId(string id)
        {
            return GetAll().AsEnumerable().Any(personInRepository => personInRepository.HasId(id));
        }

    }
}