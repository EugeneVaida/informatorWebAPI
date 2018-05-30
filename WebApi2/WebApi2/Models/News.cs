using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi2.Models
{
    public class News
    {
        public int ID { get; set; }
        public string NewsName { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }

    }



}