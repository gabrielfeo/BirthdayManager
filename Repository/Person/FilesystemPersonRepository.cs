using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Entities;
using Newtonsoft.Json;
using Validator;

namespace Repository.PersonNs
{
    internal class FilesystemPersonRepository : PersonRepository
    {
        private readonly string _peopleStorePath =
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/PeopleStore.txt";

        public FilesystemPersonRepository(IValidator<Person> personValidator) : base(personValidator)
        {
        }

        public override IEnumerable<Person> GetAll()
        {
            var serializedPeople = GetPeopleJsonFromStore();
            var people = JsonConvert.DeserializeObject<IEnumerable<Person>>(serializedPeople);
            return people ?? Enumerable.Empty<Person>();
        }

        public override Person Get(Person person)
        {
            return GetById(person.Id);
        }

        public override Person GetById(string personId)
        {
            return GetAll().FirstOrDefault(person => person.HasId(personId));
        }

        public override void Insert(Person newPerson, out bool successful)
        {
            successful = false;
            if (!CanUseStore() || !CanInsert(newPerson)) return;

            var currentPeople = GetAll();
            var peopleIncludingNewPerson = currentPeople.Append(newPerson);
            WritePeopleToStore(peopleIncludingNewPerson);

            successful = GetAll().Contains(newPerson);
        }

        public override void Update(Person changedPerson, out bool successful)
        {
            successful = false;
            if (!CanUseStore() || !CanUpdate(changedPerson)) return;

            var currentPeople = GetAll();
            var peopleWithChangedPerson = currentPeople.Where(person => !person.HasId(changedPerson.Id))
                                                       .Append(changedPerson);
            WritePeopleToStore(peopleWithChangedPerson);
            successful = GetAll().Contains(changedPerson);
        }

        public override void Delete(string personId, out bool successful)
        {
            successful = false;
            if (!CanUseStore() || !CanDelete(personId)) return;

            var currentPeople = GetAll();
            var peopleWithoutDeletedPerson = currentPeople.Where(person => !person.HasId(personId));
            WritePeopleToStore(peopleWithoutDeletedPerson);
            successful = !GetAll().Any(person => person.HasId(personId));
        }

        private string GetPeopleJsonFromStore()
        {
            if (!File.Exists(_peopleStorePath)) File.Create(_peopleStorePath);
            return File.ReadAllText(_peopleStorePath);
        }

        private void WritePeopleToStore(IEnumerable<Person> people)
        {
            var serializedPeople = JsonConvert.SerializeObject(people);
            CreateStoreIfAbsent();
            File.WriteAllText(_peopleStorePath, serializedPeople);
        }

        private void CreateStoreIfAbsent()
        {
            if (!File.Exists(_peopleStorePath)) File.Create(_peopleStorePath);
        }
        
        private bool CanUseStore()
        {
            bool canUse = false;
            try
            {
                var testStream = new FileStream(_peopleStorePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                canUse = (testStream.CanRead) && (testStream.CanWrite);
                testStream.Close();
            }
            catch (IOException exception)
            {
                canUse = false;
            }
            return canUse;
        }
    }
}