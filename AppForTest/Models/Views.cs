using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppForTest.Models
{
    public class MainView
    {
        public IEnumerable<Sentence> sentences { get; set; }
        public string word { get; set; }
        public string txtName { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}