using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace XeemAPI.Models
{
    [DataContract]
    public enum RequestStatus
    {
        [EnumMember(Value ="W")]
        Waiting = 'W',
        [EnumMember(Value = "A")]
        Accepted = 'A',
        [EnumMember(Value = "F")]
        Finished = 'F',
        [EnumMember(Value = "C")]
        Canceled = 'C'
    }
    [DataContract]
    public class Request
    {
        private int id;
        private DateTime? createdDate;
        private RequestStatus status;
        private int repairShopId;
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
        [DataMember(Name = "CreatedDate")]
        public string PublishedCreatedDate
        {
            get
            {
                return createdDate == null ? null : ((DateTime)createdDate).ToString("yyyy-MM-ddThh:mm:ss");
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

        public static Request Convert(Data.Request dto)
        {
            var temp = new Models.Request();
            temp.Id = dto.id;
            temp.createdDate = dto.createdDate;
            temp.Status = (RequestStatus)dto.status[0];
            temp.RepairShopId = (int)dto.shopId;

            return temp;
        }


        
    }
}