using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace XeemAPI.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        [Route("authenticate")]
        [HttpPost]
        public IHttpActionResult Authenticate()
        {
            var request = HttpContext.Current.Request;
            var email = request["email"];
            var password = request["password"];

            if (email == null || password == null)
            {
                return NotFound();
            }

            var token = Models.User.Authenticate(email, password);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
