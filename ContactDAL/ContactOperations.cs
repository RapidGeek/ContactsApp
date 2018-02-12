using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactDAL
{
    public class ContactOperations
    {
        public void Create(tbl_contact contact)
        {
            using (var context = new ContactContext())
            {
                context.tbl_contact.Add(contact);
                context.SaveChanges();
            }
        }

        public List<tbl_contact> Read()
        {
            using (var context = new ContactContext())
            {
                return context.tbl_contact.ToList();
            }
        }

        /// <summary>
        /// Read a contact by the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null Object on invalid read request</returns>
        public tbl_contact ReadById(int id)
        {
            // Cannot return invalid IDs
            if (id < 1) return new tbl_contact();

            using (var context = new ContactContext())
            {
                var matches = context.tbl_contact.Where(c => c.ContactID == id);
                // Create a null contact for invlid requests right now
                // Another option is to figure out an error message
                // to display
                if (matches == null || matches.Count() < 1)
                {
                    return new tbl_contact();
                }

                return matches.First();
            }
        }

        public void Update(int id, tbl_contact contact)
        {
            // Cannot update invalid IDs
            if (id < 1) return;

            using (var context = new ContactContext())
            {
                var matches = context.tbl_contact.Where(c => c.ContactID == id);
                // Ignore invalid requests right now
                // Another option is to just create the contact
                // Another option is to figure out an error message
                // to display
                if (matches == null || matches.Count() < 1) return;

                var elem = matches.First();
                elem.FirstName = contact.FirstName;
                elem.LastName = contact.LastName;
                elem.Address = contact.Address;
                elem.PhoneNumber = contact.PhoneNumber;
                elem.Birthday = contact.Birthday;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            // Cannot delete invalid IDs
            if (id < 1) return;

            using (var context = new ContactContext())
            {
                var matches = context.tbl_contact.Where(c => c.ContactID == id);
                // Ignore invalid requests right now
                // Another option is to figure out an error message
                // to display
                if (matches == null || matches.Count() < 1) return;

                var elem = matches.First();
                context.tbl_contact.Remove(elem);
                context.SaveChanges();
            }
        }
    }
}
