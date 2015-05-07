using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Serler.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public bool IsPendingUser { get; set; }
        public bool IsSystemAdmin { get; set; }
        public bool IsModerator { get; set; }
        public bool IsAnalyst { get; set; }
        public bool IsActive { get; set; }
    }
}