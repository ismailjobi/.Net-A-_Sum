using LabTask_02.DTOs;
using LabTask_02.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabTask_02.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            LadTaskEntities db = new LadTaskEntities();
            var data = db.Cources.ToList();
            var converted = Convert(data);
            return View(converted);
        }
        [HttpGet]
        public ActionResult AddCourse()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCourse(CourseDTO c)
        {
            LadTaskEntities db = new LadTaskEntities();
            if (ModelState.IsValid)
            {
                var co = Convert(c);
                db.Cources.Add(co);
                db.SaveChanges();
                return RedirectToAction("Index","Course");
            }
            return View(c);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            LadTaskEntities db = new LadTaskEntities();
            var exobj = db.Cources.Find(id);
            var converted = Convert(exobj);
            return View(converted);

        }
        [HttpPost]
        public ActionResult Edit(Cource c)
        {
            LadTaskEntities db = new LadTaskEntities();
            var exobj = db.Cources.Find(c.Id);
            //var converted = Convert(exobj);
            exobj.Id = c.Id;
            exobj.Title = c.Title;
            exobj.CreditHr = c.CreditHr;
            exobj.Type = c.Type;
            db.SaveChanges();

            return RedirectToAction("Index","Course");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            LadTaskEntities db = new LadTaskEntities();
            var exobj = db.Cources.Find(id);

            //for delete
            db.Cources.Remove(exobj);
            //db.SaveChanges();
            db.SaveChanges();

            return RedirectToAction("Index","Course");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            LadTaskEntities db = new LadTaskEntities();
            var exobj = db.Cources.Find(id);
           var converted = Convert(exobj);
            return View(converted);

        }
        
        public static CourseDTO Convert(Cource c)
        {
            return new CourseDTO()
            {
               Id = c.Id,
               Title = c.Title,
               CreditHr = c.CreditHr,
               Type = c.Type,
            };
        }
        public static Cource Convert(CourseDTO c)
        {
            return new Cource()
            {
                Id = c.Id,
                Title = c.Title,
                CreditHr = c.CreditHr,
                Type = c.Type,
            };
        }
        public static List<CourseDTO> Convert(List<Cource> Cources)
        {
            var list = new List<CourseDTO>();
            foreach (var c in Cources)
            {
                var co = Convert(c);
                list.Add(co);
            }
            return list;
        }
    }
}