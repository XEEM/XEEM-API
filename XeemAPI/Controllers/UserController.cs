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
        [Route("trans/")]
        [HttpPut]
        public IHttpActionResult AddNewTransportation()
        {
            var request = HttpContext.Current.Request;
            var token = request["api_token"];
            int id;

            if (!int.TryParse(token, out id))
            {
                return Unauthorized();
            }

            var trans = new Models.Transportation();
            try
            {
                trans.Name = request["name"];
                trans.Type = (Models.TransportationType)request["type"][0];
            } catch(Exception e)
            {
                return InternalServerError(e);
            }

            trans = Models.User.AddTransportation(id, trans);

            if(trans == null)
            {
                return InternalServerError();
            }

            return Ok(trans);
        }

        

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
