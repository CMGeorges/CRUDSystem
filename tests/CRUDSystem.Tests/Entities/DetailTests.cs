using CRUDSystem.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CRUDSystem.Tests.Entities
{
    [TestClass]
    public class DetailTests
    {
        [TestMethod]
        public void Properties_CanBeAssignedAndRead()
        {
            var birthDate = new DateTime(2001, 4, 2);
            var detail = new Detail
            {
                ID = 5,
                Fname = "Jane",
                Lname = "Doe",
                Age = 24,
                Address = "123 Main St",
                DateOfBirth = birthDate
            };

            Assert.AreEqual(5, detail.ID);
            Assert.AreEqual("Jane", detail.Fname);
            Assert.AreEqual("Doe", detail.Lname);
            Assert.AreEqual(24, detail.Age);
            Assert.AreEqual("123 Main St", detail.Address);
            Assert.AreEqual(birthDate, detail.DateOfBirth);
        }
    }
}
