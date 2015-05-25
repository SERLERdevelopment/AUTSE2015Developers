using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Serler.Models
{
    public class LazyLoadModel<T>
    {
        public IEnumerable<T> Results { get; set; }
        public int TotalCount { get; set; }
        public bool HasMore { get; set; }
    }
}