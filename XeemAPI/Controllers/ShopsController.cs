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
        public IHttpActionResult GetShops()
        {
            var request = HttpContext.Current.Request;
            var token = request["api_token"];

            int id;
            if (!int.TryParse(token, out id))
            {
                return Unauthorized();
            }

            var filtersParam = request["filters"];

            Shop[] shops = null;

            if(filtersParam == null)
            {
                shops = Shop.GetShops(id);
            } else
            {
                var filters = filtersParam.Split('-');
                shops = Shop.GetShopsByFilters(id, filters);
            }

            if (shops == null)
            {
                return InternalServerError();
            }

            return Ok(shops);
            
        }
        [Route("{shopId}/request")]
        [HttpPost]
        public IHttpActionResult RequestShop(int shopId)
        {
            var request = HttpContext.Current.Request;
            var token = request["api_token"];

            int userId;
            if (!int.TryParse(token, out userId))
            {
                return Unauthorized();
            }

            string requestToken = Shop.Request(userId, shopId);

            if(requestToken == null)
            {
                return InternalServerError();
            }

            return Ok(requestToken);
        }

        [Route("{shopId}/requests/{requestToken}/status")]
        [HttpGet]
        public IHttpActionResult GetRequestStatus(int shopId, string requestToken)
        {
            var request = HttpContext.Current.Request;
            var token = request["api_token"];
            int userId;
            if (!int.TryParse(token, out userId))
            {
                return Unauthorized();
            }
            
            int requestId;
            if (!int.TryParse(requestToken, out requestId))
            {
                return Unauthorized();
            }
            var status = Shop.GetShopRequestStatus(requestId);

            if (status == null)
            {
                return InternalServerError();
            }

            return Ok(status);
        }

        [Route("{shopId}/requests/{requestToken}/accept")]
        [HttpPost]
        public IHttpActionResult AcceptRequest(int shopId, string requestToken)
        {
            var request = HttpContext.Current.Request;
            var token = request["api_token"];
            int userId;
            if (!int.TryParse(token, out userId))
            {
                return Unauthorized();
            }

            int requestId;
            if (!int.TryParse(requestToken, out requestId))
            {
                return Unauthorized();
            }
            var user = Shop.AcceptRequest(requestId);

            if (user == null)
            {
                return InternalServerError();
            }

            return Ok(user);
        }
    }
}
