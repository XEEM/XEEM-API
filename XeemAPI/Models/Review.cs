using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using XeemAPI.Data;

namespace XeemAPI.Models
{
    [DataContract]
    public class Review
    {
        private float? rating;
        private string description;
        private User reviewer;
        private DateTime? createdDate;

        [DataMember]
        public float? Rating
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
        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }
        [DataMember]
        public User Reviewer
        {
            get
            {
                return reviewer;
            }

            set
            {
                reviewer = value;
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

        public static explicit operator Review(Data.Review dto)
        {
            if (dto == null)
                return null;

            var result = new Review();
            result.rating = (float)dto.rating;
            result.description = dto.description;
            result.reviewer = (User)dto.User;
            result.createdDate = dto.createdDate;

            return result;
        }

        public Data.Review ExportDbObject(int shopId)
        {
            var dto = new Data.Review();
            dto.shopId = shopId;
            dto.rating = this.rating;
            dto.description = this.description;
            dto.userId = this.reviewer.Id;
            dto.createdDate = this.createdDate;

            return dto;
        }

        public static Review AddReview(int shopId, Review review)
        {
            try
            {
                using (var context = new XeemEntities())
                {
                    var dto = review.ExportDbObject(shopId);
                    dto.createdDate = DateTime.Now;
                    context.Reviews.Add(dto);

                    context.SaveChanges();

                    return review;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}