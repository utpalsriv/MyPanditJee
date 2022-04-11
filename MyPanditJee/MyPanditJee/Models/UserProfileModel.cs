using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using MyPanditJee.Models;
using System.ComponentModel.DataAnnotations;

namespace MyPanditJee.Models
{
    [BsonIgnoreExtraElements]
    public class UserProfileModel
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

        [BsonElement("Gender")]
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [BsonElement("Date of Birth")]
        public string DOB { get; set; }

        [Required(ErrorMessage = "Enter address")]
        [StringLength(25, ErrorMessage = "Exceed the limit of 20 character")]
        [BsonElement("Address1")]
        public string Address1 { get; set; }

        [BsonElement("Address2")]
        [Required(AllowEmptyStrings = true)]
        public string Address2 { get; set; }

        [BsonElement("State")]
        public string State { get; set; }

        [DataType(DataType.PostalCode)]
        [BsonElement("Pin Code")]
        public string Pin { get; set; }

        [BsonElement("Country")]
        public string Country { get; set; }

        [StringLength(200, ErrorMessage = "The Name must be no longer than 200 characters!")]
        [BsonElement("About You")]
        public string AboutYOu { get; set; }

        [BsonElement("Talent Category")]
        public string TalentCategory { get; set; }

        [BsonElement("Sub Category")]
        public string SubCategory { get; set; }

        [BsonElement("Followers")]
        public int Followers { get; set; }

        [BsonElement("Following")]
        public int Following { get; set; }

        [BsonElement("Total Uploaded Files")]
        public int TotalUploadedVideo { get; set; }

        public int TotalUploadedPicture { get; set; }

        public int TotalUploadedPost { get; set; }

        public string ProfileImageId { get; set; } // link to Profile Image

        public bool HasProfileImage { get; set; }

        [BsonElement("Total Recieved Connection")]
        public int TotalConnectionReceived { get; set; }


    }
}
