using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

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
        private string longitude;
        private string latitude;
        private BasicTransportation transportation;

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
        public string Longitude
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
        public string Latitude
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

        public BasicRequest(Data.Request dto)
        {
            this.Id = dto.id;
            this.CreatedDate = dto.createdDate;
            this.Status = (RequestStatus)dto.status[0];
            this.RepairShopId = (int)dto.shopId;
            this.RequestUserId = (int)dto.CustomerTransportation.userId;
            this.transportation = BasicTransportation.Convert(dto.CustomerTransportation);
        }

        public BasicRequest()
        {

        }
    }
}