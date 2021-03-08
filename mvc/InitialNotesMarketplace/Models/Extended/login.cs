using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InitialNotesMarketplace.Models.Extended
{
    public class login
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID Required")]
        [Display(Name = "Email")]
        //[DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        //[DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}