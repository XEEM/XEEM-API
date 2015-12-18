using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace XeemAPI.Models
{
    [DataContract]
    public class Review
    {
        private float rating;
        private string description;
        private User reviewer;

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

        public static explicit operator Review(Data.Review dto)
        {
            if (dto == null)
                return null;

            var result = new Review();
            result.rating = (float)dto.rating;
            result.description = dto.description;
            result.reviewer = (User)dto.User;

            return result;
        }
    }
}