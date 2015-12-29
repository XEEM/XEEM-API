using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using XeemAPI.Models;

namespace XeemAPI.Controllers
{
    [RoutePrefix("api/requests")]
    public class RequestsController : ApiController
    {
        [Route("shops/")]
        [HttpGet]
        public IHttpActionResult GetShops()
        {
            var request = HttpContext.Current.Request;
            var token = request["api_token"];
            int id;

            if (!int.TryParse(token, out id))
            {
                return Unauthorized();
            }

            var shops = BasicShop.GetShopsByOwnerId(id);
            return Ok(shops);
        }

        [Route("")]
        [HttpPut]
        public IHttpActionResult RequestShop(string api_token, int shop_id, int transportation_id)
        {
            var request = HttpContext.Current.Request;

            int userId;
            if (!int.TryParse(api_token, out userId))
            {
                return Unauthorized();
            }

            string requestToken = Shop.Request(userId, transportation_id, shop_id);

            if (requestToken == null)
            {
                return InternalServerError();
            }

            return Ok(requestToken);
        }
    }
}