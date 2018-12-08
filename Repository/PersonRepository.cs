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
            var isNotKnownPerson = !IsKnownPerson(person);
            return (isValidPerson && isNotKnownPerson);
        }

        protected bool CanUpdate(Person changedPerson)
        {
            var isValidPerson = Validator.Validate(changedPerson);
            var isKnownPerson = IsKnownPerson(changedPerson);
            return (isValidPerson && isKnownPerson);
        }

        protected bool CanDelete(string personId)
        {
            var isKnownPerson = IsKnownPersonId(personId);
            return isKnownPerson;
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