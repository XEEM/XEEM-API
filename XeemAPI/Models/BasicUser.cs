using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace XeemAPI.Models
{
    [DataContract]
    public class BasicUser
    {
        int id;
        string name;
        string email;
        string password;
        string phone;
        string avatarUrl;
        DateTime createdDate;
        string address;
        DateTime? birthday;
        string latitude;
        string longitude;
        string token;
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
        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }
        [DataMember]
        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
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
        public DateTime CreatedDate
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
        public DateTime? Birthday
        {
            get
            {
                return birthday;
            }

            set
            {
                birthday = value;
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
        public string Token
        {
            get
            {
                return token;
            }

            set
            {
                token = value;
            }
        }

        public static explicit operator BasicUser(Data.User dto)
        {
            if (dto == null)
                return null;

            var user = new BasicUser();
            user.id = dto.id;
            user.latitude = dto.latitude;
            user.longitude = dto.longitude;
            user.name = dto.name;
            user.password = dto.password;
            user.phone = dto.phone;
            user.email = dto.email;
            user.avatarUrl = dto.avatarUrl;
            user.birthday = dto.birthday;
            user.address = dto.address;

            return user;
        }

        public BasicUser()
        {

        }
    }
}