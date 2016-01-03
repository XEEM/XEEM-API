using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using XeemAPI.Data;

namespace XeemAPI.Models
{
    [DataContract]
    public class BasicRequest
    {
        private int id;
        private DateTime? createdDate;
        private RequestStatus status;
        private int repairShopId;
        private int requestUserId;
        private decimal? longitude;
        private decimal? latitude;
        private BasicTransportation transportation;
        private string description;
        private ShopForRequest repairShop;
        [DataMember]
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        [DataMember]
        public DateTime? CreatedDate
        {
            get
            {
                return createdDate;
            }

            set
            {
                createdDate = value;
            }
        }
        [DataMember]
        public RequestStatus Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
        [DataMember]
        public int RepairShopId
        {
            get
            {
                return repairShopId;
            }

            set
            {
                repairShopId = value;
            }
        }
        [DataMember]
        public int RequestUserId
        {
            get
            {
                return requestUserId;
            }

            set
            {
                requestUserId = value;
            }
        }
        [DataMember]
        public decimal? Longitude
        {
            get
            {
                return longitude;
            }

            set
            {
                longitude = value;
            }
        }
        [DataMember]
        public decimal? Latitude
        {
            get
            {
                return latitude;
            }

            set
            {
                latitude = value;
            }
        }
        [DataMember]
        public BasicTransportation Transportation
        {
            get
            {
                return transportation;
            }

            set
            {
                transportation = value;
            }
        }
        [DataMember]
        public ShopForRequest RepairShop
        {
            get
            {
                return repairShop;
            }

            set
            {
                repairShop = value;
            }
        }

        public BasicRequest(Data.Request dto)
        {
            this.Id = dto.id;
            this.CreatedDate = dto.createdDate;
            this.Status = (RequestStatus)dto.status[0];
            this.RepairShopId = (int)dto.shopId;
            this.RequestUserId = (int)dto.CustomerTransportation.userId;
            this.latitude = dto.latitude;
            this.longitude = dto.longitude;
            this.description = dto.description;
            this.repairShop = ShopForRequest.Convert(dto.Shop);
            this.transportation = BasicTransportation.Convert(dto.CustomerTransportation);
        }

        public BasicRequest()
        {

        }

        public static BasicRequest[] GetRequestsOfShopOwner(int ownerId)
        {
            try
            {
                using (var context = new XeemEntities())
                {
                    var q = from r in context.Requests
                            where r.Shop.ownerId == ownerId
                            select r;

                    var result = Array.ConvertAll(q.ToArray(), item => new BasicRequest(item));
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}