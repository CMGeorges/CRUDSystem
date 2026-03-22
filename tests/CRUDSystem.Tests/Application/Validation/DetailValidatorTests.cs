using CRUDSystem.Application.Validation;
using CRUDSystem.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CRUDSystem.Tests.Application.Validation
{
    [TestClass]
    public class DetailValidatorTests
    {
        [TestMethod]
        public void Validate_ReturnsFailure_WhenDetailIsNull()
        {
            var result = DetailValidator.Validate(null);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Detail is required.", result.ErrorMessage);
        }

        [TestMethod]
        public void Validate_ReturnsFailure_WhenFirstNameIsMissing()
        {
            var result = DetailValidator.Validate(new Detail { Lname = "Smith", Age = 20 });

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("First name is required.", result.ErrorMessage);
        }

        [TestMethod]
        public void Validate_ReturnsFailure_WhenLastNameIsMissing()
        {
            var result = DetailValidator.Validate(new Detail { Fname = "John", Age = 20 });

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Last name is required.", result.ErrorMessage);
        }

        [TestMethod]
        public void Validate_ReturnsFailure_WhenAgeIsNegative()
        {
            var result = DetailValidator.Validate(new Detail { Fname = "John", Lname = "Smith", Age = -1 });

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Age must be a positive number.", result.ErrorMessage);
        }

        [TestMethod]
        public void Validate_ReturnsSuccess_ForValidDetail()
        {
            var result = DetailValidator.Validate(new Detail { Fname = "John", Lname = "Smith", Age = 18 });

            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.ErrorMessage);
        }
    }
}
