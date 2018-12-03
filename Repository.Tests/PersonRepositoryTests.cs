using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Entities;
using Repository;
using Repository.ValidatorNs;

namespace Repository.Tests
{

    [TestClass]
    public class PersonRepositoryTests
    {

        private Mock<IValidator<Person>> uselessValidator = Mocks.GetUselessValidator();
        private IRepository<Person> GetNewRepository() => new MemoryPersonRepository(uselessValidator.Object);

        /*
            Verifies that the Update operation actually updates the same item.
         */
        [TestMethod]
        public void UpdateShouldUpdateSamePerson()
        {
            // Arrange
            var repository = GetNewRepository();
            var birthday = new Birthday(02, 04);
            var personId = Guid.NewGuid().ToString();
            var person = new Person(personId, "John Doe", birthday);
            var samePersonWithChangedName = new Person(personId, "Mary Doe", birthday);
            repository.Insert(person, out bool additionSuccessful);

            // Act
            repository.Update(samePersonWithChangedName, out bool updateSuccessful);

            // Assert
            Assert.IsTrue(updateSuccessful);
            Assert.IsTrue(repository.GetById(personId).Name.Equals(samePersonWithChangedName.Name));
        }

        /* 
            Verifies the rule that the Update operation should fail if the IEquatable<Person> 
            implementation determines it's not the same item (thus an Update is not valid).
         */
        [TestMethod]
        public void UpdateShouldFailWhenPersonChanged()
        {
            // Arrange
            var repository = GetNewRepository();
            var name = "John Doe";
            var birthday = new Birthday(02, 04);
            var originalPersonId = Guid.NewGuid().ToString();
            var changedPersonId = Guid.NewGuid().ToString();
            var originalPerson = new Person(originalPersonId, name, birthday);
            var changedPerson = new Person(changedPersonId, name, birthday);
            repository.Insert(originalPerson, out bool _);

            // Act
            repository.Update(changedPerson, out bool updateSuccessful);

            // Assert
            Assert.IsFalse(updateSuccessful);
            Assert.IsNull(repository.Get(changedPerson));
            Assert.IsNotNull(repository.Get(originalPerson));
        }
  
    }
}
