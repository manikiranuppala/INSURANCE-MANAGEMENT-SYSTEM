using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iCareTPAWebApp.Models
{
    public class Hospital
    {
        [NotMapped]
        public int HospitalId { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Hospital Name length should be less than 20")]
        [Display(Name = "Hospital Name")]
        public string HospitalName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Address length should be less than 20")]
        public string Address { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "City length should be less than 20")]
        public string City { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "State length should be less than 20")]
        public string State { get; set; }

        [Required]
        [RegularExpression("([0-9]{1,6})", ErrorMessage = "Pincode length should be 6 or less than 6")]
        public int Pincode { get; set; }

        [Required]
        [Display(Name = "Insurer Name")]
        public string InsurerName { get; set; }

        
    }
}