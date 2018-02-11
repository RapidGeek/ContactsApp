using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ContactDAL;

namespace ContactsWebApi.Controllers
{
    public class ContactsController : ApiController
    {
        internal ContactOperations op = new ContactOperations();

        // GET api/values
        public IEnumerable<tbl_contact> Get()
        {
            return op.Read();
        }

        // GET api/values/5
        public tbl_contact Get(int id)
        {
            return op.ReadById(id);
        }

        // POST api/values
        public void Post([FromBody]tbl_contact value)
        {
            op.Create(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]tbl_contact value)
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
