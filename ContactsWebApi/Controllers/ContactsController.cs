using ContactBLL;
using System.Collections.Generic;
using System.Web.Http;

namespace ContactsWebApi.Controllers
{
    public class ContactsController : ApiController
    {
        internal ContactBL op = new ContactBL();

        // GET api/values
        public IEnumerable<Contact> Get()
        {
            return op.Read();
        }

        // GET api/values/5
        public Contact Get(int id)
        {
            return op.ReadById(id);
        }

        // POST api/values
        public void Post([FromBody]Contact value)
        {
            op.Create(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]Contact value)
        {
            op.Update(id, value);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            op.Delete(id);
        }
    }
}
