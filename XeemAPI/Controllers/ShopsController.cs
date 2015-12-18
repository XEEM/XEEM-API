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
    [RoutePrefix("api/shops")]
    public class ShopsController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAllShops()
        {
            var request = HttpContext.Current.Request;
            var token = request["api_token"];

            int id;
            if (!int.TryParse(token, out id))
            {
                return Unauthorized();
            }

            var shops = Shop.GetShops(id);

            if (shops == null)
            {
                return InternalServerError();
            }

            return Ok(shops);
            
        }
        [Route("request")]
        [HttpPost]
        public IHttpActionResult RequestShop()
        {
            var request = HttpContext.Current.Request;
            var token = request["api_token"];
            var shopId = request["shop_id"];

            return Ok();
        }
    }
}
