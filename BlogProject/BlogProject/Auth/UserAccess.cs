using BlogProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Auth
{
    public class UserAccess : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user =(User)httpContext.Session["user"];
            if (user != null && user.Role.RoleType.Equals("User")) { 
                return true;
            }
            return false;
        }
    }
}