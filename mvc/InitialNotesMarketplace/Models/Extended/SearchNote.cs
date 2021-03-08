using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InitialNotesMarketplace.Models.Extended
{
    public class SearchNote
    {
        public ICollection<NoteCategory> ListCategories { get; set; }
        public ICollection<NoteType> ListTypes { get; set; }
        public ICollection<Country> ListCountries { get; set; }
        public ICollection<string> ListUniversities { get; set; }
        public ICollection<string> ListCourses { get; set; }
    }
}