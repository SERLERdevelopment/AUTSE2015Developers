using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Serler.Models
{
    public class RegisterViewModel : LoginViewModel
    {
        [Required]
        public string Password2 { get; set; }
    }
}