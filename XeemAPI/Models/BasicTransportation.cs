using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace XeemAPI.Models
{
    [DataContract]
    public enum TransportationType
    {
        [EnumMember(Value = "B")]
        Bike = 'B',
        [EnumMember(Value = "C")]
        Car = 'C',
        [EnumMember(Value = "S")]
        Scooter = 'S',
        [EnumMember(Value = "M")]
        Motorbike = 'M'
    }
    [DataContract]
    public class BasicTransportation
    {
        private int id;
        private string name;
        private TransportationType type;
        private List<string> imageUrls;
        private BasicUser owner;

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
        [JsonConverter(typeof(StringEnumConverter))]
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
        [DataMember]
        public BasicUser Owner
        {
            get
            {
                return owner;
            }

            set
            {
                owner = value;
            }
        }

        public static BasicTransportation Convert(Data.CustomerTransportation dto)
        {
            var result = new BasicTransportation();

            result.Id = dto.id;
            result.Name = dto.Transportation.name;
            result.Type = (TransportationType)dto.Transportation.type[0];
            result.imageUrls = new List<string>();
            foreach (var image in dto.Transportation.TransportationPhotos)
            {
                result.imageUrls.Add(image.imageUrl);
            }

            result.owner = (BasicUser)dto.Transportation.CustomerTransportations.First().User;
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
    }
}