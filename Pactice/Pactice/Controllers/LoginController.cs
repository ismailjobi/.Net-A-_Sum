using Pactice.DTOs;
using Pactice.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace Pactice.Controllers
{
    public class LoginController : Controller
    {
        PacticeDemoEntities db = new PacticeDemoEntities();
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            if (Request["ReturnUrl"] != null)
            {
                ViewBag.URL = Request["ReturnUrl"];
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginDTO LoginData,string URL)
        {
            if (ModelState.IsValid)
            {
                var user = (from u in db.Users
                            where u.UserId.Equals(LoginData.UserName) &&
                            u.Password.Equals(LoginData.Password)
                            select u).FirstOrDefault();
                if (user == null)
                {
                    TempData["Msg"] = "Please Check your Credensial";
                    return View(LoginData);
                }
                Session["user"] = user;
                TempData["Msg"] = "Login Successfull";
                var role = db.Roles.Find(user.RoleId);
                if (role.RoleName.Equals("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                if (URL != null && !URL.Equals(""))
                    return Redirect(URL);
                else
                    return RedirectToAction("Index", "Student");


            }
            return View(LoginData);
        }
        public ActionResult LogOut()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}