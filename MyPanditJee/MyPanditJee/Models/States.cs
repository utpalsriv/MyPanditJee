using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyPanditJee.Models
{
    [BsonIgnoreExtraElements]
    public class States
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("StateName")]
        public string StateName { get; set; }

        [BsonElement("CountryName")]
        public string CountryName { get; set; }
    }
}
