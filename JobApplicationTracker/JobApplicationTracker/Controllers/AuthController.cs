using BLL.DTO;
using BLL.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace JobApplicationTracker.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("api/login")]
        public HttpResponseMessage Login(LoginDTO login)
        {
            try
            {
                var data = AuthService.Authenticate(login.UserName, login.Password);
                if(data != null )
                {
                    HttpContext.Current.Session["user"] = data;
                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, false);
                }
                
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/login" });
            }
        }
    }
}
