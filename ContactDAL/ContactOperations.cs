using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public tbl_contact ReadById(int id)
        {
            using (var context = new ContactContext())
            {
                return context.tbl_contact.Where(c => c.ContactID == id).FirstOrDefault();
            }
        }

        public void Update(int id, tbl_contact contact)
        {
            using (var context = new ContactContext())
            {
                var elem = context.tbl_contact.Where(c => c.ContactID == id).FirstOrDefault();
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
            using (var context = new ContactContext())
            {
                var elem = context.tbl_contact.Where(c => c.ContactID == id).FirstOrDefault();
                context.tbl_contact.Remove(elem);
                context.SaveChanges();
            }
        }
    }
}
