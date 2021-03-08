using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InitialNotesMarketplace.Models.Extended
{
    public partial class SellerNote
    {
        public int ID { get; set; }
        public int SellerID { get; set; }
        public int Status { get; set; }
        public Nullable<int> ActionedBy { get; set; }
        public string AdminRemarks { get; set; }
        public Nullable<System.DateTime> PublishedDate { get; set; }
        [Required]
        [Display(Name = "Title*")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Category*")]
        public int Category { get; set; }
        [Display(Name = "Display Picture")]
        public HttpPostedFileBase DisplayPicture { get; set; }
        [Display(Name = "Type")]
        public Nullable<int> NoteType { get; set; }
        [Display(Name = "Number of Pages")]
        public Nullable<int> NumberOfPages { get; set; }
        [Required]
        [Display(Name = "Description*")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Institution Name*")]
        public string UniversityName { get; set; }
        public Nullable<int> Country { get; set; }
        [Display(Name = "Course Name")]
        public string Course { get; set; }
        [Display(Name = "Course Code")]
        public string CourseCode { get; set; }
        public string Professor { get; set; }
        public bool IsPaid { get; set; }
        public Nullable<decimal> SellingPrice { get; set; }
        [Display(Name = "Note Preview")]
        public HttpPostedFileBase NotesPreview { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}