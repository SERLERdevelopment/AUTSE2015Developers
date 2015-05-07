using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Serler.Models
{
    public class PagedViewModel<T>
    {
        public IEnumerable<T> Result { get; set; }
        public int TotalCount { get; set; }
        public int MyProperty { get; set; }
        public SearchModel SearchModel { get; set; }
        public List<PaginationModel> PaginationModel { get; set; }
    }

    public class SearchModel
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string SearchText { get; set; }
    }

    public class PaginationModel
    {
        public string DisplayLabel { get; set; }
        public string Url { get; set; }
        public bool IsEnable { get; set; }
        public bool IsActive { get; set; }
    }
}