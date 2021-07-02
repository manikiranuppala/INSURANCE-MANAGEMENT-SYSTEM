using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iCareTPAWebApp.Models
{
    public class User
    {
        [Required]
        [StringLength(20, ErrorMessage = "First Name length should be less than 20")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Last Name length should be less than 20")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "EmailAddress length should be less than 20")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [Display(Name ="EmailAddress")]
        public string EmailId { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Password length should be Greater than 6 and less than 20",MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}