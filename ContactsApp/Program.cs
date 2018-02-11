using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactDAL;

namespace ContactsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var contact = new tbl_contact()
            {
                FirstName = "Joe",
                LastName = "Smith",
                Address = "123",
                PhoneNumber = "555",
                Birthday = new DateTime(1980, 1, 1)
            };

            var op = new ContactOperations();
            //op.Create(contact);
            //op.Update(2, contact);
            op.Delete(2);
            var list = op.Read();

            foreach(var elem in list)
            {
                Console.WriteLine($"{elem.FirstName} {elem.LastName} {elem.Address} {elem.PhoneNumber} {elem.Birthday}");
            }

            // Pause at the end
            Console.Read();
        }
    }
}
