using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using XeemAPI.Data;

namespace XeemAPI.Models
{
    [DataContract]
    public enum TransportationType
    {
        [EnumMember]
        Bike = 'B',
        [EnumMember]
        Car = 'C',
        [EnumMember]
        Scooter = 'S',
        [EnumMember]
        Motorbike = 'M'
    }

    [DataContract]
    public class Transportation
    {
        private int id;
        private string name;
        private TransportationType type;

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
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        [DataMember]
        public TransportationType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public static explicit operator Transportation(Data.Transportation dto)
        {
            var result = new Transportation();

            result.Id = dto.id;
            result.Name = dto.name;
            result.Type = (TransportationType)dto.type[0];

            return result;
        }

        public Data.Transportation ExportToDataObject()
        {
            var dto = new Data.Transportation();
            dto.id = this.Id;
            dto.name = this.Name;
            dto.type = new string((char)this.Type, 1);

            return dto;
        }

        public static Transportation[] GetUserTransportations(int userId)
        {
            using(var context = new XeemEntities())
            {
                var q = from t in context.Transportations
                        join u in context.CustomerTransportations on t.id equals u.id
                        where u.userId == userId
                        select t;

                        
            }

            return null;
        }
    }
}