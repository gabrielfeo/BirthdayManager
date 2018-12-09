using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Validator;

namespace Repository
{
    internal class MemoryPersonRepository : PersonRepository
    {
        private ICollection<Person> _people = new HashSet<Person>();

        internal MemoryPersonRepository(IValidator<Person> personValidator)
            : base(personValidator)
        {
        }

        public override ICollection<Person> GetAll() => _people;
        public override Person Get(Person person) => GetById(person.Id);

        public override Person GetById(string personId)
        {
            return _people.FirstOrDefault(person => person.HasId(personId));
        }

        public override void Insert(Person newPerson, out bool successful)
        {
            if (CanInsert(newPerson))
            {
                _people.Add(newPerson);
                successful = _people.Contains(newPerson);
            }
            else
            {
                successful = false;
            }
        }

        public override void Update(Person changedPerson, out bool successful)
        {
            if (CanUpdate(changedPerson))
            {
                Delete(changedPerson.Id, out bool removalSuccessful);
                if (removalSuccessful) _people.Add(changedPerson);
                successful = removalSuccessful && _people.Contains(changedPerson);
            }
            else
            {
                successful = false;
            }
        }

        public override void Delete(string personId, out bool successful)
        {
            if (CanDelete(personId))
            {
                var person = GetById(personId);
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