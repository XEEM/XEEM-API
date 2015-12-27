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
        [EnumMember(Value ="B")]
        Bike = 'B',
        [EnumMember(Value ="C")]
        Car = 'C',
        [EnumMember(Value ="S")]
        Scooter = 'S',
        [EnumMember(Value ="M")]
        Motorbike = 'M'
    }

    [DataContract]
    public class Transportation
    {
        private int id;
        private string name;
        private TransportationType type;
        private List<Request> requests;
        private List<string> imageUrls;

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
        [DataMember]
        public List<Request> Requests
        {
            get
            {
                return requests;
            }

            set
            {
                requests = value;
            }
        }
        [DataMember]
        public List<string> ImageUrls
        {
            get
            {
                return imageUrls;
            }

            set
            {
                imageUrls = value;
            }
        }

        public static Transportation Convert(Data.CustomerTransportation dto)
        {
            var result = new Transportation();

            result.Id = dto.id;
            result.Name = dto.Transportation.name;
            result.Type = (TransportationType)dto.Transportation.type[0];
            result.requests = new List<Models.Request>();
            
            foreach(var request in dto.Requests)
            {
                result.requests.Add(Models.Request.Convert(request));
            }

            result.imageUrls = new List<string>();
            foreach(var image in dto.Transportation.TransportationPhotos)
            {
                result.imageUrls.Add(image.imageUrl);
            }

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