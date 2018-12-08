using System;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Validator;

// ReSharper disable MemberCanBePrivate.Global

namespace Validator.Tests
{
    [TestClass]
    public class PersonValidatorTests
    {
        [TestInitialize]
        public void Initialize()
        {
            Validator = new PersonValidator();
        }

        public IValidator<Person> Validator;

        public readonly string ValidId = Guid.NewGuid().ToString();
        public const string ValidName = "John Doe";
        public readonly Birthday ValidBirthday = new Birthday(1, 1);

        [TestMethod]
        public void ValidationShouldSucceedWithValidProperties()
        {
            // Arrange
            var personWithValidProperties = new Person(ValidId, ValidName, ValidBirthday);

            // Act
            var isValid = Validator.Validate(personWithValidProperties);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValidationShouldFailWithInvalidId()
        {
            // Arrange
            var invalidId = "invalidGuid";
            var personWithInvalidId = new Person(invalidId, ValidName, ValidBirthday);

            // Act
            var isValid = Validator.Validate(personWithInvalidId);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValidationShouldFailWithEmptyName()
        {
            // Arrange
            var emptyName = string.Empty;
            var personWithEmptyName = new Person(ValidId, emptyName, ValidBirthday);

            // Act
            var isValid = Validator.Validate(personWithEmptyName);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValidationShouldFailWithInvalidBirthday()
        {
            // Arrange
            var invalidBirthday = new Birthday(0, 0);
            var personWithInvalidBirthday = new Person(ValidId, ValidName, invalidBirthday);

            // Act
            var isValid = Validator.Validate(personWithInvalidBirthday);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValidationShouldFailWithNullId()
        {
            // Arrange
            var personWithNullId = new Person(null, ValidName, ValidBirthday);

            // Act
            var isValid = Validator.Validate(personWithNullId);

            // Assert
            Assert.IsFalse(isValid);
        }

        public void ValidationShouldFailWithNullName()
        {
            // Arrange
            var personWithNullName = new Person(ValidId, null, ValidBirthday);

            // Act
            var isValid = Validator.Validate(personWithNullName);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}