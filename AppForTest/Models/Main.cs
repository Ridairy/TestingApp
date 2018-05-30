using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppForTest.Models
{
    //CodeFirst main table structure
    public class Sentence
    {
        [Key,Required]
        public int id { get; set; }
        public string sentence{ get; set; }
        public string searchhWord { get; set; }
        public int matchs { get; set; }
    }
    //Pagination needs
    public class PageInfo
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalItems { get; set; }
        public int totalPages { get {
                return (int)Math.Ceiling((decimal)totalItems / pageSize);
            } }
    }

}