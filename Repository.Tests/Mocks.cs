using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Entities;
using Repository;
using Validator;

namespace Repository.Tests
{
    internal static class Mocks
    {
        public static Mock<IValidator<Person>> GetUselessValidator()
        {
            var mockValidator = new Mock<IValidator<Person>>();
            mockValidator
                .Setup(validator => validator.Validate(It.IsAny<Person>()))
                .Returns(true);
            return mockValidator;
        }
    }
}