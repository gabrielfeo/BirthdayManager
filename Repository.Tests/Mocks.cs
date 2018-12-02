using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Entities;
using Repository;
using Repository.ValidatorNs;

namespace Repository.Tests
{
    internal static class Mocks
    {
        public static Mock<IValidator<Person>> GetUselessValidator()
        {
            var mockValidator = new Mock<IValidator<Person>>();
            var anyPerson = It.IsAny<Person>();
            mockValidator
                .Setup(validator => validator.Validate(anyPerson))
                .Returns(true);
            return mockValidator;
        }
    }
}