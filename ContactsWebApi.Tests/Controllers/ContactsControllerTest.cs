using ContactBLL;
using ContactsWebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ContactsWebApi.Tests.Controllers
{
    public static class Random
    {
        private static RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        private static int lengthOfStrings = 5;

        public static int LengthOfStrings
        {
            get { return lengthOfStrings; }
            set { if (value >= 4) lengthOfStrings = value; }
        }

        public static string GetString
        {
            get
            {
                StringBuilder buffer = new StringBuilder();
                bool capitalizeFirstLetter = true;
                var bytes = new Byte[LengthOfStrings * 8];
                crypto.GetBytes(bytes);

                int i = 0;
                int a = (int)'a';
                int z = (int)'z';
                while (buffer.Length < LengthOfStrings)
                {
                    while (bytes[i] < a)
                    {
                        bytes[i] += 26;
                    }

                    while (bytes[i] > z)
                    {
                        bytes[i] -= 26;
                    }

                    var letter = (char)bytes[i];

                    
                    if (capitalizeFirstLetter)
                    {
                        letter = letter.ToString().ToUpper().ToCharArray()[0];
                        capitalizeFirstLetter = false;
                    }

                    buffer.Append(letter);
                    i++;
                }

                return buffer.ToString();
            }
        }

        public static string GetAddress
        {
            get
            {
                StringBuilder buffer = new StringBuilder();
                var bytes = new Byte[2];
                crypto.GetBytes(bytes);

                buffer.Append(BitConverter.ToUInt16(bytes, 0));
                buffer.Append(" ");
                buffer.Append(GetString);

                return buffer.ToString();
            }
        }

        public static string GetPhoneNumber
        {
            get
            {
                StringBuilder buffer = new StringBuilder();
                var bytes = new Byte[30];
                crypto.GetBytes(bytes);

                buffer.Append(BitConverter.ToUInt16(bytes, 0) % 999);
                buffer.Append("-");
                buffer.Append(BitConverter.ToUInt16(bytes, 0) % 999);
                buffer.Append("-");
                buffer.Append(BitConverter.ToUInt16(bytes, 0) % 9999);

                return buffer.ToString();
            }
        }

        public static DateTime GetDateOfBirth
        {
            get
            {
                var bytes = new Byte[8];
                crypto.GetBytes(bytes);
                var yearYalue = BitConverter.ToUInt16(bytes, 0) % (DateTime.Now.Year - 1930);
                var monthYalue = (BitConverter.ToUInt16(bytes, 2) % 12) + 1;
                var dayModValue = (monthYalue == 2) ?
                    28 :
                    ((monthYalue == 9 || monthYalue == 4 || monthYalue == 6 || monthYalue == 11) ?
                        30 :
                        31);
                var dayYalue = BitConverter.ToUInt16(bytes, 4) % dayModValue;
                var year = 1930 + yearYalue;
                var month = monthYalue;
                var day = dayYalue + 1;

                return new DateTime(year, month, day);
            }
        }
    }

    [TestClass]
    public class ContactsControllerTest
    {
        static Contact NullContact = new Contact();

        [TestInitialize]
        public void Initialize()
        {
            ContactBL controller = new ContactBL();

            if (controller.Read().Count < 15)
            {
                for (int i = 0; i < 15; i++)
                {
                    var contact = new Contact()
                    {
                        FirstName = Random.GetString,
                        LastName = Random.GetString,
                        Address = Random.GetAddress,
                        PhoneNumber = Random.GetPhoneNumber,
                        Birthday = Random.GetDateOfBirth
                    };

                    controller.Create(contact);
                }
            }
        }

        [TestMethod]
        public void Get()
        {
            // Arrange
            ContactsController controller = new ContactsController();

            // Act
            IEnumerable<Contact> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            ContactsController controller = new ContactsController();
            var last = controller.Get().Last();

            // Act
            Contact result = controller.Get(last.ContactID);

            // Assert
            Assert.AreEqual(last.ContactID, result.ContactID);
        }

        [TestMethod]
        public void GetByIdInvalidIdGreaterThanTableLength()
        {
            // Arrange
            ContactsController controller = new ContactsController();
            var last = controller.Get().Last();

            // Act
            Contact result = controller.Get(last.ContactID + 1);

            // Assert
            // This is 0 because of the NullContact which is returned
            Assert.IsTrue(NullContact == result);
        }

        [TestMethod]
        public void GetByIdInvalidIdLessThanMinimumTableId()
        {
            // Arrange
            ContactsController controller = new ContactsController();

            // Act
            Contact result = controller.Get(0);

            // Assert
            // This is 0 because of the NullContact which is returned
            Assert.IsTrue(NullContact == result);
        }

        [TestMethod]
        public void GetByIdInvalidIdDeletedId()
        {
            // Arrange
            ContactsController controller = new ContactsController();
            var contact = new Contact()
            {
                FirstName = "Joe",
                LastName = "Smith",
                Address = "123",
                PhoneNumber = "555",
                Birthday = new DateTime(1980, 1, 1)
            };
            controller.Post(contact);
            var last = controller.Get().Last();

            // Act
            controller.Delete(last.ContactID);
            Contact result = controller.Get(last.ContactID);

            // Assert
            // This is 0 because of the NullContact which is returned
            Assert.IsTrue(NullContact == result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            ContactsController controller = new ContactsController();
            var contact = new Contact()
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
            var putMe = controller.Get().Last();
            var contact = new Contact()
            {
                FirstName = "Joe",
                LastName = "Smoe",
                Address = "123",
                PhoneNumber = "555",
                Birthday = new DateTime(1980, 1, 1)
            };

            // Act
            controller.Put(putMe.ContactID, contact);

            // Assert
            var last = controller.Get().Last();
            Assert.AreEqual("Smoe", last.LastName);
        }

        [TestMethod]
        public void PutSameEntryMultipleTimes()
        {
            // Arrange
            ContactsController controller = new ContactsController();
            var id = controller.Get().Last().ContactID;
            var contact = new Contact()
            {
                FirstName = "Joe",
                LastName = "Smoe",
                Address = "123",
                PhoneNumber = "555",
                Birthday = new DateTime(1980, 1, 1)
            };

            // Act
            controller.Put(id, contact);
            controller.Put(id, contact);
            controller.Put(id, contact);
            controller.Put(id, contact);
            controller.Put(id, contact);

            // Assert
            var last = controller.Get(id);
            Assert.AreEqual("Smoe", last.LastName);
        }

        [TestMethod]
        public void PutInvalidIdGreaterThanTableLength()
        {
            // Arrange
            ContactsController controller = new ContactsController();
            var last = controller.Get().Last();
            var contact = new Contact()
            {
                FirstName = "Joe",
                LastName = "Bob",
                Address = "123",
                PhoneNumber = "555",
                Birthday = new DateTime(1980, 1, 1)
            };

            // Act
            controller.Put(last.ContactID + 1, contact);

            // Assert
            var one = controller.Get(last.ContactID + 1);
            Assert.AreNotEqual("Bob", one.LastName);
        }

        [TestMethod]
        public void DeleteLastElement()
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

        [TestMethod]
        public void DeleteMiddleElement()
        {
            // Arrange
            ContactsController controller = new ContactsController();
            var allContacts = controller.Get().ToList();
            var allContactsCount = allContacts.Count;
            var allContactsMiddle = allContactsCount / 2;
            var deleteMe = allContacts[allContactsMiddle];

            // Act
            controller.Delete(deleteMe.ContactID);

            // Assert
            var middle = controller.Get(allContactsMiddle);
            Assert.IsTrue(middle.ContactID != deleteMe.ContactID);
        }

        [TestMethod]
        public void DeleteSameEntryMultipleTimes()
        {
            // Arrange
            ContactsController controller = new ContactsController();
            var deleteMe = controller.Get().Last();

            // Act
            controller.Delete(deleteMe.ContactID);
            controller.Delete(deleteMe.ContactID);
            controller.Delete(deleteMe.ContactID);
            controller.Delete(deleteMe.ContactID);
            controller.Delete(deleteMe.ContactID);

            // Assert
            var last = controller.Get().Last();
            Assert.IsTrue(last.ContactID != deleteMe.ContactID);
        }
    }
}
