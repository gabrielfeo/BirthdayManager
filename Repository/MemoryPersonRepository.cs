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
        public override Person Get(string personId) => _people.First(person => person.HasId(personId));

        public override void Create(Person newPerson, out bool successful)
        {
            base.Create(newPerson, out successful);
            _people.Add(newPerson);
        }

        public override void Update(Person changedPerson, out bool successful)
        {
            base.Update(changedPerson, out successful);
            _people.Add(changedPerson);
        }

        public override void Delete(string personId, out bool successful)
        {
            base.Delete(personId, out successful);
            var person = Get(personId);
            _people.Remove(person);
        }

    }
}