using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace InitialNotesMarketplace.Models.Extended
{
    public class Note
    {
        
        public SellerNote sellerNote { get; set; }
        public SellerNotesAttachment sellerNotesAttachment { get; set; }
    }
}