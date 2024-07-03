using AutoMapper;
using Microsoft.Ajax.Utilities;
using Pactice.DTOs;
using Pactice.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Pactice.Controllers
{
    public class UserController : Controller
    {
        PacticeDemoEntities db =new PacticeDemoEntities();
        // GET: User
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(UserDTO userdata)
        {
            if (ModelState.IsValid) {
                var roledata =(from r in db.Roles
                               where r.RoleName.Equals("Student")
                               select r).FirstOrDefault(); 
                var mapper =CustomMapper();
                var data = mapper.Map<User>(userdata);
                data.RoleId = roledata.Id;
                data.Name=userdata.FirstName.Trim()+" "+userdata.LastName.Trim();
                db.Users.Add(data);
                TempData["Msg"] = "Account Created";
                db.SaveChanges();

                return RedirectToAction("Index","Login");
            }
            return View(userdata);
        }

        public static Mapper CustomMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserDTO,User>();
            });
            return new Mapper(config);
        }
    }
}