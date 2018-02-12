using ContactDAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactBLL
{
    public class ContactBL
    {
        /// <summary>
        /// Create a new contact
        /// </summary>
        /// <param name="contact"></param>
        public void Create(Contact contact)
        {
            // Cannot create an invalid contact
            if (object.ReferenceEquals(contact, null))
            {
                throw new ArgumentException(
                    $"Contacts cannot be created with a null reference.");
            }

            var op = new ContactOperations();
            contact.FirstName = contact.FirstName.Trim();
            contact.LastName = contact.LastName.Trim();
            contact.Address = contact.Address.Trim();
            contact.PhoneNumber = contact.PhoneNumber.Trim();
            op.Create(Convert(contact));
        }

        /// <summary>
        /// Read all contacts
        /// </summary>
        /// <returns></returns>
        public List<Contact> Read()
        {
            var op = new ContactOperations();
            var list = Convert(op.Read());
            return list;
        }

        /// <summary>
        /// Read a contact by their ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Contact ReadById(int id)
        {
            // return a blank contact
            if (id < 1) return new Contact();

            var op = new ContactOperations();
            var item = Convert(op.ReadById(id));
            return item;
        }

        /// <summary>
        /// Update a contact by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contact"></param>
        public void Update(int id, Contact contact)
        {
            // return without updating for invalid arguments
            if (id < 1) return;
            if (object.ReferenceEquals(contact, null)) return;

            var op = new ContactOperations();
            contact.FirstName = contact.FirstName.Trim();
            contact.LastName = contact.LastName.Trim();
            contact.Address = contact.Address.Trim();
            contact.PhoneNumber = contact.PhoneNumber.Trim();
            op.Update(id, Convert(contact));
        }

        /// <summary>
        /// Delete a contact by ID
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            if (id < 1) return;

            var op = new ContactOperations();
            op.Delete(id);
        }

        private tbl_contact Convert(Contact c)
        {
            if (object.ReferenceEquals(c, null)) return new tbl_contact();

            var contact = new tbl_contact()
            {
                ContactID = c.ContactID,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Address = c.Address,
                PhoneNumber = c.PhoneNumber,
                Birthday = c.Birthday
            };

            return contact;
        }

        private Contact Convert(tbl_contact c)
        {
            if (object.ReferenceEquals(c, null)) return new Contact();

            var contact = new Contact()
            {
                ContactID = c.ContactID,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Address = c.Address,
                PhoneNumber = c.PhoneNumber,
                Birthday = c.Birthday
            };

            return contact;
        }

        private List<tbl_contact> Convert(List<Contact> c)
        {
            return c.Select(i => Convert(i)).ToList();
        }

        private List<Contact> Convert(List<tbl_contact> c)
        {
            return c.Select(i => Convert(i)).ToList();
        }
    }
}
