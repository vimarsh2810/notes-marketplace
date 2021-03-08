using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InitialNotesMarketplace.Models.Extended
{
    public partial class signup
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name Required")]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name Required")]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID Required")]
        [Display(Name = "Email")]
        //[DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        //[DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        [Compare("Password", ErrorMessage = "Both passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}