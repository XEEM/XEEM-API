using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace XeemAPI.Models
{
    [DataContract]
    public class Quote
    {
        private int id;
        private string name;
        private float price;

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
        public float Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        public static explicit operator Quote(Data.ShopQuote dto)
        {
            if (dto == null)
                return null;

            var quote = new Quote();
            quote.id = dto.Quote.id;
            quote.name = dto.Quote.name;
            quote.price = (float)dto.QuotePrices.Where(shopQuote => shopQuote.endDate == null).First().price;

            return quote;
        }
    }
}