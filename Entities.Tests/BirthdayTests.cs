using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities;

namespace Tests
{
    [TestClass]
    public class BirthdayTests
    {

        private readonly DateTime _today = DateTime.Now;

        [TestMethod]
        public void GetNextDateBeforeBirthdayShouldReturnThisYearBirthday()
        {
            // Arrange
            var pastBirthday = new Birthday(_today.Day-1, _today.Month);
            var expectedYear = _today.Year-1;

            // Act
            var nextDate = pastBirthday.GetNextDate();

            // Assert
            Assert.AreEqual(expectedYear, nextDate.Year);
        }

        [TestMethod]
        public void GetNextDateAfterBirthdayShouldReturnNextYearBirthday()
        {
            // Arrange
            var futureBirthday = new Birthday(_today.Day+1, _today.Month);
            var expectedYear = _today.Year+1;

            // Act
            var nextDate = futureBirthday.GetNextDate();

            // Assert
            Assert.AreEqual(expectedYear, nextDate.Year);
        }

    }
}
