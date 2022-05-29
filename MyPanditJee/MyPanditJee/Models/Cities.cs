using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyPanditJee.Models
{
    [BsonIgnoreExtraElements]
    public class Cities
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("city")]
        public string CityName { get; set; }

        [BsonElement("state")]
        public string StateName { get; set; }
    }
}
