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
        public IHttpActionResult RequestShop()
        {
            var request = HttpContext.Current.Request;
            var api_token = request["api_token"];
            int userId;
            if (!int.TryParse(api_token, out userId))
            {
                return Unauthorized();
            }

            var temp = request["shop_id"];
            int shop_id;
            if (!int.TryParse(temp, out shop_id))
                return BadRequest();

            temp = request["transportation_id"];
            int transportation_id;
            if (!int.TryParse(temp, out transportation_id))
                return BadRequest();

            temp = request["latitude"];
            decimal latitude;
            if (!decimal.TryParse(temp, out latitude))
                return BadRequest();

            temp = request["longitude"];
            decimal longitude;
            if (!decimal.TryParse(temp, out longitude))
                return BadRequest();

            var description = request["description"];
            if (description == null)
                return BadRequest();

            string requestToken = Shop.Request(userId, transportation_id, shop_id, latitude, longitude, description);

            if (requestToken == null)
            {
                return InternalServerError();
            }

            return Ok(requestToken);
        }

        [Route("{request_id}/accept")]
        [HttpPost]
        public IHttpActionResult AcceptRequest(int request_id)
        {
            var request = HttpContext.Current.Request;
            var api_token = request["api_token"];
            int userId;
            if (!int.TryParse(api_token, out userId))
            {
                return Unauthorized();
            }

            var basicRequest = Shop.AcceptRequest(request_id);
            if (basicRequest == null)
            {
                return InternalServerError();
            }

            return Ok(basicRequest);
        }

        [Route("{request_id}/finish")]
        [HttpPost]
        public IHttpActionResult FinishRequest(int request_id)
        {
            var request = HttpContext.Current.Request;
            var api_token = request["api_token"];
            int userId;
            if (!int.TryParse(api_token, out userId))
            {
                return Unauthorized();
            }

            var temp = request["shop_id"];
            int shopId;
            if (!int.TryParse(temp, out shopId))
                return BadRequest();

            var basicRequest = Shop.FinishRequest(request_id);
            if (basicRequest == null)
            {
                return InternalServerError();
            }

            return Ok(basicRequest);
        }

        [Route("{request_id}/cancel")]
        [HttpPost]
        public IHttpActionResult CancelRequest(int request_id)
        {
            var request = HttpContext.Current.Request;
            var api_token = request["api_token"];
            int userId;
            if (!int.TryParse(api_token, out userId))
            {
                return Unauthorized();
            }

            //var temp = request["shop_id"];
            //int shopId;
            //if (!int.TryParse(temp, out shopId))
            //    return BadRequest();

            var basicRequest = Shop.CancelRequest(request_id);
            if (basicRequest == null)
            {
                return InternalServerError();
            }

            return Ok(basicRequest);
        }

        [Route("{request_id}")]
        [HttpGet]
        public IHttpActionResult GetRequestStatus(int request_id)
        {
            var request = HttpContext.Current.Request;
            var api_token = request["api_token"];
            int userId;
            if (!int.TryParse(api_token, out userId))
            {
                return Unauthorized();
            }

            var basicRequest = Shop.GetRequestById(request_id);
            if (basicRequest == null)
            {
                return InternalServerError();
            }

            return Ok(basicRequest);
        }
    }
}