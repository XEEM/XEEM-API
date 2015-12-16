using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace XeemAPI.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        // GET: api/Users
        [Route("")]
        public IHttpActionResult Get()
        {
            var users = Models.User.FindAll();

            if(users == null)
            {
                return InternalServerError();
            }

            return Ok(users);
        }

        // GET: api/Users/5
        public IHttpActionResult Get(int id)
        {
            var user = Models.User.FindById(id);
            if (user == null) {
                return NotFound();
            }

            return Json(user);
        }

        // POST: api/Users
        [HttpPost]
        public IHttpActionResult Post(int id, [FromUri]Models.User user)
        {
            var updatedProperties = new List<string>();
            if(user.Name != null)
            {
                updatedProperties.Add("name");
            }

            if(user.Phone != null)
            {
                updatedProperties.Add("phone");
            }

            if(user.Address != null)
            {
                updatedProperties.Add("address");
            }

            if(user.Birthday != null)
            {
                updatedProperties.Add("birthday");
            }
            
            if(!Models.User.Update(user, updatedProperties))
            {
                return NotFound();
            }

            return Ok();
        }

        // PUT: api/Users/5
        [HttpPut]
        public IHttpActionResult Put([FromBody]string email, [FromBody]string phone, [FromBody]string password)
        {
            var user = new Models.User();
            user.Email = email;
            user.Phone = phone;
            user.Password = password;

            if (!Models.User.Insert(user))
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}
