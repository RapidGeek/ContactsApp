using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactsWebApi;
using ContactsWebApi.Controllers;
using ContactDAL;

namespace ContactsWebApi.Tests.Controllers
{
    [TestClass]
    public class ContactsControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            ContactsController controller = new ContactsController();

            // Act
            IEnumerable<tbl_contact> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            ContactsController controller = new ContactsController();

            // Act
            tbl_contact result = controller.Get(1);

            // Assert
            Assert.AreEqual(1, result.ContactID);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            ContactsController controller = new ContactsController();
            var contact = new tbl_contact()
            {
                FirstName = "Joe",
                LastName = "Smith",
                Address = "123",
                PhoneNumber = "555",
                Birthday = new DateTime(1980, 1, 1)
            };

            // Act
            controller.Post(contact);

            // Assert
            var last = controller.Get().Last();
            Assert.IsTrue(last == contact);
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            ContactsController controller = new ContactsController();
            var contact = new tbl_contact()
            {
                FirstName = "Joe",
                LastName = "Smoe",
                Address = "123",
                PhoneNumber = "555",
                Birthday = new DateTime(1980, 1, 1)
            };

            // Act
            controller.Put(1, contact);

            // Assert
            var one = controller.Get(1);
            Assert.AreEqual("Smoe", one.LastName);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            ContactsController controller = new ContactsController();
            var deleteMe = controller.Get().Last();

            // Act
            controller.Delete(deleteMe.ContactID);

            // Assert
            var last = controller.Get().Last();
            Assert.IsTrue(last.ContactID != deleteMe.ContactID);
        }
    }
}
