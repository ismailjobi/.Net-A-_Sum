using AutoMapper;
using PMS.Auth;
using PMS.DTOs;
using PMS.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Controllers
{
    public class UserController : Controller
    {
        PMS_Sm24_AEntities db = new PMS_Sm24_AEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(UserDTO UserData)
        {
            if (ModelState.IsValid)
            {
                var role =(from type in db.Types
                           where type.Type1.Equals("Customer")
                           select type).FirstOrDefault();
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<UserDTO, User>();
                });
                var mapper = new Mapper(config);
                var data = mapper.Map<User>(UserData);
                data.TypeId = role.Id;
                db.Users.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index","Login");
            }
            return View(UserData);
        }
        [Logged]
        [HttpGet]
        public ActionResult Profile()
        {
            var data = (User)Session["User"];
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<EF.Type, TypeDTO>();
            });
            var mapper = new Mapper(config);
            var ConvertedData = mapper.Map<UserDTO>(data);
            return View(ConvertedData);
        }
        [Logged]
        [HttpGet]
        public ActionResult ChangePassword() { 
          return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordDTO passdata)
        {
            var userId = ((User)Session["User"]).Id;

            var user=db.Users.Find(userId);

            if (ModelState.IsValid) {
                if (!user.Pass.Equals(passdata.Password))
                {
                    TempData["Msg"] = "Current password does not match.";
                    return View(passdata);
                }
                if (!passdata.NewPassword.Equals(passdata.ConfirmPassword))
                {
                    TempData["Msg"] = "New password and confirm password do not match.";
                    return View(passdata);
                }
                user.Pass = passdata.NewPassword;
                TempData["Msg"] = "Password successfully changed.";
                db.SaveChanges();
                return View();
            }
           
            return View(passdata);
        }

    }
}