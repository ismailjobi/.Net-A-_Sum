using PMS.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Auth
{
    public class CustomerAccess : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = (User)httpContext.Session["User"];
            if (user!=null && user.Type.Type1.Equals("Customer"))
            {

                return true;
            }
            return false;
        }
    }
}