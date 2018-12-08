using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Validator;

namespace Repository
{
    internal abstract class PersonRepository : IRepository<Person>
    {
        protected IValidator<Person> Validator;

        protected PersonRepository(IValidator<Person> personValidator)
        {
            Validator = personValidator;
        }

        public abstract ICollection<Person> GetAll();
        public abstract Person Get(Person person);
        public abstract Person GetById(string personId);

        public abstract void Insert(Person newPerson, out bool successful);
        public abstract void Update(Person changedPerson, out bool successful);
        public abstract void Delete(string personId, out bool successful);

        protected bool CanInsert(Person person)
        {
            var isValidPerson = Validator.Validate(person);
            var isImpostor = IsKnownPerson(person);
            return (!isValidPerson || isImpostor);
        }

        protected bool CanUpdate(Person changedPerson)
        {
            var isValidPerson = Validator.Validate(changedPerson);
            var isKnown = IsKnownPerson(changedPerson);
            return (!isValidPerson || !isKnown);
        }

        protected bool CanDelete(string personId)
        {
            var isKnown = IsKnownPersonId(personId);
            return !isKnown;
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