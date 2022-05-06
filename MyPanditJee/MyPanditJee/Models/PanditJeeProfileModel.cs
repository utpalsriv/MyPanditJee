using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPanditJee.Models
{
    public class PanditJeeProfileModel
    {
        [Required(ErrorMessage = "You must enter a Name!")]
        [StringLength(25, ErrorMessage = "The Name must be no longer than 20 characters!")]
        [BsonElement("Employeer Name")]
        public string PanditJeeName { get; set; }

        [EmailAddress]
        [BsonElement("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must enter a CompanyName!")]
        [StringLength(25, ErrorMessage = "The Name must be no longer than 20 characters!")]
        [BsonElement("MandirName")]
        public string MandirName { get; set; }

       

        [BsonElement("Phone No")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string PhoneNo { get; set; }

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

        [StringLength(50, ErrorMessage = "The Name must be no longer than 50 characters!")]
        [BsonElement("Specialization ")]
        public string Specialization { get; set; }
    }
}
