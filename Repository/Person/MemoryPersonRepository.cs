using System.Collections.Generic;
using System.Linq;
using Validator;

namespace Repository.PersonNs
{
    internal class MemoryPersonRepository : PersonRepository
    {
        private ICollection<Entities.Person> _people = new HashSet<Entities.Person>();

        internal MemoryPersonRepository(IValidator<Entities.Person> personValidator)
            : base(personValidator)
        {
        }

        public override IEnumerable<Entities.Person> GetAll() => _people;
        public override Entities.Person Get(Entities.Person person) => GetById(person.Id);

        public override Entities.Person GetById(string personId)
        {
            return _people.FirstOrDefault(person => person.HasId(personId));
        }

        public override void Insert(Entities.Person newPerson, out bool successful)
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

        public override void Update(Entities.Person changedPerson, out bool successful)
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