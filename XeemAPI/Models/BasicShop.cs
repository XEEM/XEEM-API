using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace XeemAPI.Models
{
    [DataContract]
    public class BasicShop
    {
        private int id;
        private string name;
        private string address;
        private string phone;
        private bool? isAvailable;
        private string latitude;
        private string longitude;
        private string avatarUrl;
        private DateTime? createdDate;
        private ShopType type;
        private BasicUser owner;
        private List<Review> reviews;
        private List<Quote> quotes;
        private float rating;
        private bool isFavorited;
        private List<BasicRequest> requests;

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
        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }
        [DataMember]
        public string Phone
        {
            get
            {
                return phone;
            }

            set
            {
                phone = value;
            }
        }
        [DataMember]
        public bool? IsAvailable
        {
            get
            {
                return isAvailable;
            }

            set
            {
                isAvailable = value;
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
        public string AvatarUrl
        {
            get
            {
                return avatarUrl;
            }

            set
            {
                avatarUrl = value;
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
        public ShopType Type
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
        [DataMember]
        public List<Review> Reviews
        {
            get
            {
                return reviews;
            }

            set
            {
                reviews = value;
            }
        }
        [DataMember]
        public List<Quote> Quotes
        {
            get
            {
                return quotes;
            }

            set
            {
                quotes = value;
            }
        }
        [DataMember]
        public float Rating
        {
            get
            {
                return rating;
            }

            set
            {
                rating = value;
            }
        }
        [DataMember]
        public bool IsFavorited
        {
            get
            {
                return isFavorited;
            }

            set
            {
                isFavorited = value;
            }
        }
        [DataMember]
        public List<BasicRequest> Requests
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

        public static BasicShop Convert(Data.Shop dto)
        {
            if (dto == null)
                return null;

            var result = new BasicShop();
            result.id = dto.id;
            result.name = dto.name;
            result.address = dto.address;
            result.phone = dto.phone;
            result.isAvailable = dto.isAvailable;
            result.latitude = dto.latitude;
            result.longitude = dto.longitude;
            result.avatarUrl = dto.avatarUrl;
            result.createdDate = dto.createdDate;
            result.type = (ShopType)dto.type[0];
            result.owner = (BasicUser)dto.User;
            result.quotes = new List<Quote>();

            foreach (var dtoQuote in dto.ShopQuotes)
            {
                result.quotes.Add((Quote)dtoQuote);
            }

            result.reviews = new List<Review>();
            foreach (var dtoReview in dto.Reviews)
            {
                result.reviews.Add((Review)dtoReview);
            }

            result.rating = (float)result.reviews.Select(r => r.Rating).DefaultIfEmpty(0).Average();
           
            result.requests = new List<BasicRequest>();
            foreach (var request in dto.Requests)
            {
                result.requests.Add(new BasicRequest(request));
            }
            return result;
        }
        public static BasicShop[] GetShopsByOwnerId(int ownerId)
        {
            try
            {
                using (var context = new Data.XeemEntities())
                {
                    var q = from s in context.Shops 
                            where s.ownerId == ownerId
                            select s;

                    var result = Array.ConvertAll(q.ToArray(), item => Convert(item));

                    
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