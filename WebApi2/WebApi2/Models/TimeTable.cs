using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi2.Models
{
    public class TimeTable
    {
        public int? Id { get; set; }
        public string Group { get; set; }
        public string Date { get; set; }
        public string Room { get; set; }
        public string Subject { get; set; }        
    }
}