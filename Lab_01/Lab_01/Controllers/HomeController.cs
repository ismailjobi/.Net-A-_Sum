using Lab_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab_01.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Personal info = new Personal()
            {
                Name="Ismail Jobi Ullah",
                Email="niloy@gmail.com",
                Phone="01600000000",
                Degree="CSE",
                Expertice="GPT"
            };
            ViewBag.Personals = new Personal[] { info };
            return View();
        }

        public ActionResult Education()
        {
            Education details1 = new Education()
            {
                Title="SSC",
                Year=2018,
                Group="Science",
                Inst="X school"

            };
            Education details2 = new Education()
            {
                Title = "HSC",
                Year = 2020,
                Group = "Science",
                Inst = "X collage"

            };
            ViewBag.Educations = new Education[] { details1, details2 };
            return View();
        }

        public ActionResult Project()
        {
            Project details1 = new Project()
            {
                Title = "Digital Banking Hub",
                Desc = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                "dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea " +
                "commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Course = "Adv. WEB Tech"

            };
            ViewBag.Projects = new Project[] { details1 };
            return View();
        }
        public ActionResult Certification()
        {
            return View();

        }
        public ActionResult Refer()
        {
            return View();
        }
    }
}