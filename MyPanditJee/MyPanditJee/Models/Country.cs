using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyPanditJee.Models
{
    [BsonIgnoreExtraElements]
    public class Country
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("CountryName")]

        public string CountryName { get; set; }
    }
}
