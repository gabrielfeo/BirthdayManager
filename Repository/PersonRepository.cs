using System;
using System.Collections.Generic;
using System.Linq;

using Entities;

namespace Repository
{
    public sealed class PersonRepository : IRepository<Person>
    {
        private PersonRepository() { }
        public static PersonRepository Instance { get; }

        private HashSet<Person> people = new HashSet<Person>();

        public ICollection<Person> GetAll() => people;
        public Person Get(Guid personId) => people.First(person => person.HasId(personId));
        
        public void Create(Person newPerson, out bool successful)
        {
            successful = people.Add(newPerson);
        }
        public void Update(Person changedPerson, out bool successful)
        {
            bool removalSuccessful, additionSuccessful;
            removalSuccessful = people.Remove(changedPerson);
            additionSuccessful = (removalSuccessful) ? people.Add(changedPerson) : false;
            successful = removalSuccessful && additionSuccessful;
        }
        public void Delete(Guid personId, out bool successful) 
        {
            var personToBeDeleted = people.First(person => person.HasId(personId));
            successful = people.Remove(personToBeDeleted);
        }

    }
}