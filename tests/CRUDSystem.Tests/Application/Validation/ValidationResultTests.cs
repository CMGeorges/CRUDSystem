using CRUDSystem.Application.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CRUDSystem.Tests.Application.Validation
{
    [TestClass]
    public class ValidationResultTests
    {
        [TestMethod]
        public void Success_CreatesValidResult()
        {
            var result = ValidationResult.Success();

            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.ErrorMessage);
        }

        [TestMethod]
        public void Failure_CreatesInvalidResult_WithMessage()
        {
            var result = ValidationResult.Failure("boom");

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("boom", result.ErrorMessage);
        }
    }
}
