using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Serler.Models
{
    public class PaperViewModel
    {
        public int PaperId { get; set; }
        public string PaperTitle { get; set; }
        public int Rating { get; set; }
        public int NoOfPeopleRated { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
        public string Publisher { get; set; }
        public string Abstract { get; set; }
        public string Reference { get; set; }
        public bool IsActive { get; set; }
        public string Category { get; set; }
        public string PaperLink { get; set; }
    }

}