using BlogProject.DTOs;
using BlogProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Controllers
{
    public class LoginController : Controller
    {
        DemoTaskEntities1 db = new DemoTaskEntities1 ();
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginDTO LoginData)
        {
            if (ModelState.IsValid)
            {
                var user = (from u in db.Users
                            where u.UName.Equals(LoginData.UserName) &&
                            u.Pass.Equals(LoginData.Password)
                            select u).FirstOrDefault();
                if (user == null)
                {
                    TempData["Msg"] = "Please Check your Credensial";
                    return View(LoginData);
                }
                Session["user"] = user;
                TempData["Msg"] = "Login Successfull";
                if (user.Status.Equals(1))
                {
                    
                    return RedirectToAction("Index", "User");
                }

                TempData["Msg"] = "Your Account is Deactivate";

            }
            return View(LoginData);
        }
        [HttpGet]
        public ActionResult LogOut() {
            Session["user"] = null;
            return RedirectToAction("Index");
        }
    }
}