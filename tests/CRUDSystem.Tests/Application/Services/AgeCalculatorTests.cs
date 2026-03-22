using CRUDSystem.Application.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CRUDSystem.Tests.Application.Services
{
    [TestClass]
    public class AgeCalculatorTests
    {
        [TestMethod]
        public void CalculateAge_ReturnsAge_WhenBirthdayAlreadyPassed()
        {
            var result = AgeCalculator.CalculateAge(new DateTime(2000, 3, 1), new DateTime(2026, 3, 21));

            Assert.AreEqual(26, result);
        }

        [TestMethod]
        public void CalculateAge_SubtractsOne_WhenBirthdayHasNotPassedYet()
        {
            var result = AgeCalculator.CalculateAge(new DateTime(2000, 10, 1), new DateTime(2026, 3, 21));

            Assert.AreEqual(25, result);
        }
    }
}
