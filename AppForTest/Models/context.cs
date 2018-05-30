using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AppForTest.Models
{
    public class SentContext:DbContext
    {
        public SentContext():base("SentContext")
        {

        }
        public DbSet<Sentence> Sentences { get; set; }
    }
}