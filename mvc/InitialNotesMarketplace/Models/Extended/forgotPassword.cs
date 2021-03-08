using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InitialNotesMarketplace.Models.Extended
{
    public class forgotPassword
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID Required")]
        [Display(Name = "Email")]
        //[DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }
    }
}