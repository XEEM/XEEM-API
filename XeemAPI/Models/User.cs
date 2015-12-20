
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using XeemAPI.Data;

namespace XeemAPI.Models
{
    [DataContract]
    public class User
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
        List<Transportation> transporations;

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
        public List<Transportation> Transporations
        {
            get
            {
                return transporations;
            }

            set
            {
                transporations = value;
            }
        }

        public static explicit operator User(Data.User dto)
        {
            if (dto == null)
                return null;

            User user = new User();
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

            user.transporations = new List<Transportation>();
            foreach (var trans in dto.CustomerTransportations)
            {
                user.transporations.Add((Transportation)trans.Transportation);
            }

            return user;
        }

        public Data.User ExportToDataObject()
        {
            var dto = new Data.User();
            dto.address = this.address;
            dto.avatarUrl = this.avatarUrl;
            dto.birthday = this.birthday;
            dto.createdDate = this.createdDate;
            dto.email = this.email;
            dto.id = this.id;
            dto.latitude = this.latitude;
            dto.longitude = this.longitude;
            dto.name = this.name;
            dto.password = this.password;
            dto.phone = this.phone;

            return dto;
        }

        public static string Authenticate(string email, string password)
        {
            try
            {
                using (var model = new Data.XeemEntities())
                {
                    var q = from u in model.Users
                            where u.email.Equals(email) && u.password.Equals(password)
                            select u;

                    return ((User)q.First<Data.User>()).id.ToString();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static User[] FindAll()
        {
            try
            {
                using (var model = new Data.XeemEntities())
                {
                    return Array.ConvertAll(model.Users.ToArray(), item => (User)item);
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public static User FindById(int id)
        {
            using (var model = new Data.XeemEntities())
            {
                var user = model.Users.Find(id);
                
                if (user == null)
                    return null;

                var result = (User)user;

                return result;
            }
        }

        public static bool Insert(User user)
        {
            try
            {
                using (var model = new Data.XeemEntities())
                {
                    var dto = user.ExportToDataObject();
                    model.Users.Add(dto);
                    model.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public static bool Update(User user, List<string> updatedProperties)
        {
            try {
                using (var model = new Data.XeemEntities())
                {
                    var objectContext = ((IObjectContextAdapter)model).ObjectContext;
                    var dto = user.ExportToDataObject();
                    model.Users.Attach(dto);
                    var entry = objectContext.ObjectStateManager.GetObjectStateEntry(dto);
                    
                    foreach(var columnName in updatedProperties)
                    {
                        entry.SetModifiedProperty(columnName);
                    }

                    model.SaveChanges();                    
                }
            }catch(Exception e)
            {
                return false;
            }

            return true;
        }

        public static Transportation AddTransportation(int userId, Transportation trans)
        {
            try
            {
                using (var context = new XeemEntities())
                {
                    var dto = trans.ExportToDataObject();
                    // check if the transporation is exists
                    var q = from t in context.Transportations
                                  where t.name == dto.name && t.type == dto.type
                                  select t;
                    
                    if(q.Count() == 0)
                    {
                        // if not exists, insert id
                        context.Transportations.Add(dto);
                        context.SaveChanges();    
                    } else
                    {
                        dto = q.First();
                    }

                    // get the transporation id
                    // get the user
                    var user = context.Users.Find(userId);
                    // add it to user's transporatation
                    var customerTrans = new CustomerTransportation();
                    customerTrans.userId = userId;
                    customerTrans.transId = dto.id;
                    customerTrans.createdDate = DateTime.Now;

                    user.CustomerTransportations.Add(customerTrans);
                    context.SaveChanges();

                    return trans;
                }
            }catch(Exception e)
            {
                return null;
            }
        }
    }
}