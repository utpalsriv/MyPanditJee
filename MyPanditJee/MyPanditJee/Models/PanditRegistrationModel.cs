using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyPanditJee.Models
{
    public class PanditRegistrationModel
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("Name")]
        public string Name { get; set; }

        [Required]
        [Phone]
        [BsonElement("Phone")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        [BsonElement("Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [BsonElement("Password")]
        public string Password { get; set; }

        [NotMapped]
        [BsonElement("ConfirmPassword")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm Password does't match ")]
        public string ConfirmPassword { get; set; }

        [BsonElement("CreatedOn")]
        public DateTime Created { get; set; }

        [BsonElement("UpdatedOn")]
        public DateTime LastUpdated { get; set; }

        [BsonElement("RegisterType")]
        public int RegistrationType { get; set; }
    }
}
