﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using XeemAPI.Data;

namespace XeemAPI.Models
{
    [DataContract]
    public enum RequestStatus
    {
        [EnumMember]
        Waiting = 'W',
        [EnumMember]
        Accepted = 'A',
        [EnumMember]
        Finished = 'F',
        [EnumMember]
        Canceled = 'C'
    }

    [DataContract]
    public enum ShopType
    {
        [EnumMember(Value = "Gas Statiion")]
        GasStation = 'G',
        [EnumMember(Value ="Repair Shop")]
        RepairShop = 'R',
        [EnumMember(Value ="Spare Parts Shop")]
        SparePartsShop = 'S'
    }

    [DataContract]
    public class Shop
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
        private User owner;
        private List<Review> reviews;
        private List<Quote> quotes;
        private float rating;
        private bool isFavorited;

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
        public User Owner
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

        public static Shop Convert(Data.Shop dto, int userId)
        {
            if (dto == null)
                return null;

            var result = new Shop();
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
            result.owner = (User)dto.User;
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

            result.rating = result.reviews.Select(r => r.Rating).DefaultIfEmpty(0).Average();
            result.isFavorited = dto.Favorites.Where(f => f.userId == userId).Count() == 1;
            
            return result;
        }

        public Data.User ExportToDataObject()
        {
            var dto = new Data.User();
            dto.address = this.address;
            dto.avatarUrl = this.avatarUrl;            
            dto.createdDate = this.createdDate;
            dto.id = this.id;
            dto.latitude = this.latitude;
            dto.longitude = this.longitude;
            dto.name = this.name;
            dto.phone = this.phone;

            return dto;
        }

        public static Shop[] GetShops(int userId)
        {
            try
            {
                using (var context = new Data.XeemEntities())
                {
                    var result = Array.ConvertAll(context.Shops.ToArray(), item => Convert(item, userId));
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static string Request(int userId, int shopId)
        {
            var request = new Data.Request();
            try
            {
                using (var context = new XeemEntities())
                {
                    request.userId = userId;
                    request.shopId = shopId;
                    request.createdDate = DateTime.Now;
                    request.status = new string((char)RequestStatus.Waiting, 1);
                    
                    context.Requests.Add(request);
                    context.SaveChanges();
                }
            }
            catch(Exception e)
            {
                return null;
            }

            return request.id.ToString();
        }

        public static RequestStatus? GetShopRequestStatus(int requestId)
        {
            try
            {
                using (var context = new XeemEntities())
                {
                    var q = from r in context.Requests
                            where r.id == requestId
                            select r;

                    var result = (RequestStatus?)q.First().status[0];
                    return result;
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public static User AcceptRequest(int requestToken)
        {
            try
            {
                using (var context = new XeemEntities())
                {
                    var request = context.Requests.Find(requestToken);
                    request.status = new string((char)RequestStatus.Accepted, 1);
                    var user = (User)request.User;
                    context.SaveChanges();

                    return user;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}