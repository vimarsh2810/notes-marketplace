using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InitialNotesMarketplace.Models.Extended
{
    public class SellerNotesAttachment
    {
        public int ID { get; set; }
        public int NoteID { get; set; }
        public string FileName { get; set; }
        [Required]
        [Display(Name = "Upload Notes*")]
        public HttpPostedFileBase FilePath { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}