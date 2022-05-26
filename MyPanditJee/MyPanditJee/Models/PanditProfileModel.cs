using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPanditJee.Models
{

    [BsonIgnoreExtraElements]
    public class PanditProfileModel
    {
        [Required(ErrorMessage = "You must enter a Name!")]
        [StringLength(25, ErrorMessage = "The Name must be no longer than 20 characters!")]
        [BsonElement("Name")]
        public string Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        [BsonElement("Phone")]
        public string Phone { get; set; }

        [EmailAddress]
        [BsonElement("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter address")]
        [StringLength(25, ErrorMessage = "Exceed the limit of 20 character")]
        [BsonElement("Address1")]
        public string Address1 { get; set; }

        [BsonElement("Address2")]
        [Required(AllowEmptyStrings = true)]
        public string Address2 { get; set; }

        [BsonElement("States")]
        public string States { get; set; }

        [BsonElement("Cities")]
        public string Cities { get; set; }

        [DataType(DataType.PostalCode)]
        [BsonElement("Pin Code")]
        public string Pin { get; set; }

        [BsonElement("Country")]
        public string Country { get; set; }

        public string ProfileImageId { get; set; } // link to Profile Image

        public bool HasProfileImage { get; set; }
    }
}
