using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPanditJee.Models
{
    public class BookPanditModel
    {

        [BsonElement("Pooja Name")]
        public string PoojaName { get; set; }


        [BsonElement("Address")]
        public string Address { get; set; }

        [BsonElement("Date")]
        public DateTime Date { get; set; }

        [BsonElement("Time")]
        public string Time { get; set; }


        [BsonElement("MobileNo")]
        public string MobileNo { get; set; }


        [BsonElement("Email")]
        public string Email { get; set; }
    }
}
