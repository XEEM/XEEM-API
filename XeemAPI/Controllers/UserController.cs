using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using XeemAPI.Models;

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

        

        [Route("update")]
        [HttpPost]
        public IHttpActionResult UpdateUserInfo()
        {
            var request = HttpContext.Current.Request;

            var token = request["api_token"];
            int userId;

            if (!int.TryParse(token, out userId))
            {
                return Unauthorized();
            }

            var updatedProperties = new List<string>();
            var user = new Models.User();
            user.Id = userId;
            user.Email = request["email"];
            user.Phone = request["phone"];
            user.Password = request["password"];
            if(request["birthday"] != null)
            {
                user.Birthday = DateTime.Parse(request["birthday"]);
            }
            
            user.Name = request["name"];
            user.Address = request["address"];

            var file = request.Files["avatar"];
            if (file != null)
            {
                user.AvatarUrl = FileHandler.AddFile(file);
                updatedProperties.Add("avatarUrl");
            }

            if (user.Email != null)
            {
                updatedProperties.Add("email");
            }
            if (user.Password != null)
            {
                updatedProperties.Add("password");
            }
            if (user.Name != null)
            {
                updatedProperties.Add("name");
            }

            if (user.Phone != null)
            {
                updatedProperties.Add("phone");
            }

            if (user.Address != null)
            {
                updatedProperties.Add("address");
            }

            if (user.Birthday != null)
            {
                updatedProperties.Add("birthday");
            }

            var updatedObject = Models.User.Update(user, updatedProperties);
            if (updatedObject == null)
            {
                return NotFound();
            }

            updatedObject = Models.User.FindById(userId);
            return Ok(updatedObject);
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

            return Ok(user);
        }
    }
}
