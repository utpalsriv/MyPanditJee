using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyPanditJee.Models
{
    public class LoginModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [BsonElement("Email")]
        public string Email { get; set; }                      

        [Required]
        [DataType(DataType.Password)]
        [BsonElement("Password")]
        public string Password { get; set; }

        public int UserType { get; set; }

        public bool? LoginStatus { get; set; }
    }
}
