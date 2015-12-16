using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace XeemAPI.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetCurrentUserInfo()
        {
            var request = HttpContext.Current.Request;
            var token = request["api_token"];
            int id;

            if (!int.TryParse(token, out id))
            {
                return Unauthorized();
            }

            var user = Models.User.FindById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Json(user);
        }
    }
}
