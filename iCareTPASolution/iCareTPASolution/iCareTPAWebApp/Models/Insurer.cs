using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iCareTPAWebApp.Models
{
    public class Insurer
    {
        [NotMapped]
        public int InsureId { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Insurer Name length should be less than 20")]
        [Display(Name = "Insurer Name")]
        public string InsurerName { get; set; }

        [Required]
        [RegularExpression("([0-9]{1,4})", ErrorMessage = "Registration Number length should be 4 or less than 4")]
        [Display(Name = "Registration No")]
        public int RegistrationNo { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "Insurer Name length should be less than 80")]
        [Display(Name = "HeadOffice")]
        public string HeadOffice { get; set; }
    }
}