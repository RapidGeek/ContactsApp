using ContactDAL;
using System.Collections.Generic;
using System.Linq;

namespace ContactBLL
{
    public class ContactBL
    {
        public void Create(Contact contact)
        {
            var op = new ContactOperations();
            contact.FirstName = contact.FirstName.Trim();
            contact.LastName = contact.LastName.Trim();
            contact.Address = contact.Address.Trim();
            contact.PhoneNumber = contact.PhoneNumber.Trim();
            op.Create(Convert(contact));
        }

        public List<Contact> Read()
        {
            var op = new ContactOperations();
            var list = Convert(op.Read());
            return list;
        }

        public Contact ReadById(int id)
        {
            var op = new ContactOperations();
            var item = Convert(op.ReadById(id));
            return item;
        }

        public void Update(int id, Contact contact)
        {
            var op = new ContactOperations();
            contact.FirstName = contact.FirstName.Trim();
            contact.LastName = contact.LastName.Trim();
            contact.Address = contact.Address.Trim();
            contact.PhoneNumber = contact.PhoneNumber.Trim();
            op.Update(id, Convert(contact));
        }

        public void Delete(int id)
        {
            var op = new ContactOperations();
            op.Delete(id);
        }

        private tbl_contact Convert(Contact c)
        {
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
