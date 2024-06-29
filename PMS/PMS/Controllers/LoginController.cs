using PMS.DTOs;
using PMS.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Controllers
{
    public class LoginController : Controller
    {
        PMS_Sm24_AEntities db = new PMS_Sm24_AEntities();
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
                            where u.Uname.Equals(LoginData.Uname) &&
                            u.Pass.Equals(LoginData.Pass)
                            select u).FirstOrDefault();
                if (user == null)
                {
                    TempData["Msg"] = "Please Check your Credensial";
                    return View(LoginData);
                }
                Session["user"] = user;
                TempData["Msg"] = "Login Successfull";
                var role = db.Types.Find(user.TypeId);
                if (role.Type1.Equals("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("Index", "Customer");


            }
            return View(LoginData);  
        }
        public ActionResult LogOut()
        {
            Session["user"]= null;
            return RedirectToAction("Index", "Home");
        }
            
    }
}