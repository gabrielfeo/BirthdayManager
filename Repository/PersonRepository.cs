using System;
using System.Collections;
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

        public abstract IEnumerable<Person> GetAll();
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
            var isKnownPerson = IsKnownPersonId(changedPerson.Id);
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

        public IEnumerable<Person> Search(string query)
        {
            var people = GetAll();
            var queryAsDateTime = ParseDateTimeFrom(query);
            var queryAsInteger = ParseIntFrom(query);

            bool NameAsQuery(Person person) => person.Name.ToLower().Contains(query.ToLower());
            bool DateTimeAsQuery(Person person) => person.Birthday.GetNextDate().Equals(queryAsDateTime);
            bool YearAsQuery(Person person) => person.Birthday.GetNextDate().Year.Equals(queryAsInteger);
            bool MonthAsQuery(Person person) => person.Birthday.Month.Equals(queryAsInteger);
            bool DayAsQuery(Person person) => person.Birthday.Day.Equals(queryAsInteger);

            var peopleMatchingName = people.Where(NameAsQuery);
            var peopleMatchingFullDate = people.Where(DateTimeAsQuery);
            var peopleMatchingYear = people.Where(YearAsQuery);
            var peopleMatchingMonth = people.Where(MonthAsQuery);
            var peopleMatchingDay = people.Where(DayAsQuery);

            return peopleMatchingName
                   .Concat(peopleMatchingFullDate)
                   .Concat(peopleMatchingYear)
                   .Concat(peopleMatchingMonth)
                   .Concat(peopleMatchingDay);
        }

        private int ParseIntFrom(string query)
        {
            int.TryParse(query, out var integer);
            return integer;
        }

        private DateTime ParseDateTimeFrom(string query)
        {
            DateTime.TryParse(query, out var dateTime);
            return dateTime;
        }
    }
}