using Serler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace Serler.Services
{
    public class AuthenticationService
    {
        private HttpContext CurrentContext;

        public AuthenticationService()
        {
            CurrentContext = HttpContext.Current;
        }

        public void SignIn(LoginViewModel model)
        {
            var ticket = new FormsAuthenticationTicket(1, model.Email, DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout), false, "");
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            cookie.HttpOnly = true;
            CurrentContext.Response.Cookies.Add(cookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}