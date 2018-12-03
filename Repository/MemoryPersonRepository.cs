using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Repository.ValidatorNs;

namespace Repository
{
    internal class MemoryPersonRepository : PersonRepository
    {
        private ICollection<Person> _people = new HashSet<Person>();

        internal MemoryPersonRepository(IValidator<Person> personValidator)
        : base(personValidator) { }

        public override ICollection<Person> GetAll() => _people;
        public override Person Get(Person person) => GetById(person.id);
        public override Person GetById(string personId)
        {
            return _people.FirstOrDefault(person => person.HasId(personId));
        }

        public override void Insert(Person newPerson, out bool successful)
        {
            base.Insert(newPerson, out successful);
            _people.Add(newPerson);
            successful = _people.Contains(newPerson);
        }

        public override void Update(Person changedPerson, out bool successful)
        {
            base.Update(changedPerson, out successful);
            Delete(changedPerson.id, out bool removalSuccessful);
            if (removalSuccessful) _people.Add(changedPerson);
            successful = removalSuccessful && _people.Contains(changedPerson);
        }

        public override void Delete(string personId, out bool successful)
        {
            base.Delete(personId, out successful);
            var person = GetById(personId);
            if (person != null)
            {
                bool removalSuccessful = _people.Remove(person);
                successful = removalSuccessful;
            }
            else
            {
                successful = false;
            }
        }

    }
}