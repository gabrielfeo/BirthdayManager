using System;
using Entities;

namespace Validator
{
    internal class PersonValidator : IValidator<Person>
    {

        private Person _person;

        public bool Validate(Person person)
        {
            _person = person;
            bool isPersonValid = (IsIdValid() && IsNameValid() && IsBirthdayValid());
            return isPersonValid;
        }

        private bool IsIdValid()
        {
            bool isValidGuid = Guid.TryParse(_person.id, out Guid _);
            return isValidGuid;
        }

        private bool IsNameValid()
        {
            bool isNullOrEmpty = string.IsNullOrEmpty(_person.Name);
            bool isWhitespace = string.IsNullOrWhiteSpace(_person.Name);
            return (!isNullOrEmpty && !isWhitespace);
        }

        private bool IsBirthdayValid()
        {
            string allegedBirthdayDate = $"{DateTime.Today.Year}/{_person.Birthday.Month}/{_person.Birthday.Day}";
            bool isValidDate = DateTime.TryParse(allegedBirthdayDate, out DateTime _);
            return isValidDate;
        }

    }
}
