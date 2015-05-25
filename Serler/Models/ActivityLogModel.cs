using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Serler.Models
{
    public class ActivityLogModel
    {
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public string JsonData { get; set; }
        public int? RefId { get; set; }
        public string Activity { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}