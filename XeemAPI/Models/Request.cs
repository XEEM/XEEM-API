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

    public class Request
    {
        private int id;
        private DateTime? createdDate;
        private RequestStatus status;
        private int repairShopId;

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
            temp.CreatedDate = dto.createdDate;
            temp.Status = (RequestStatus)dto.status[0];
            temp.RepairShopId = (int)dto.shopId;

            return temp;
        }


        
    }
}