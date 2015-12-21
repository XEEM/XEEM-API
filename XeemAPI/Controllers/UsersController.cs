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
        

        // PUT: api/Users/5
        [Route("")]
        [HttpPut]
        public IHttpActionResult RegisterUser()
        {
            var request = HttpContext.Current.Request;

            var user = new Models.User();
            if (request["email"] == null || request["password"] == null)
            {
                return BadRequest();
            }

            user.Email = request["email"];
            user.Phone = request["phone"];
            user.Password = request["password"];
            user.Birthday = DateTime.Parse(request["birthday"]);
            user.Name = request["name"];
            user.Address = request["address"];
            user.AvatarUrl = request["avatarUrl"];

            if (!Models.User.Insert(user))
            {
                return InternalServerError();
            }

            return Ok(user);
        }
    }
}
