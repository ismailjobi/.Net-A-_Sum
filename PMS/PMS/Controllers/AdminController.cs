using AutoMapper;
using PMS.Auth;
using PMS.DTOs;
using PMS.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PMS.Controllers
{
    [AdminAccess]
    public class AdminController : Controller
    {
        PMS_Sm24_AEntities db = new PMS_Sm24_AEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Role()
        {
            var data=db.Types.ToList();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EF.Type, TypeDTO>();
            });
            var mapper = new Mapper(config);
            var RoleData = mapper.Map<List<TypeDTO>>(data);
            return View(RoleData);
        }
        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateRole(TypeDTO RoleData)
        {
            if (ModelState.IsValid) {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<TypeDTO, EF.Type>();
                });
                var mapper = new Mapper(config);
                var data = mapper.Map<EF.Type>(RoleData);
                db.Types.Add(data);
                TempData["Msg"] = "Role Created.";
                db.SaveChanges();
                return RedirectToAction("Role");
            }

            return View(RoleData);
        }
        
        public ActionResult Category()
        {
            var data = db.Categorys.ToList();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Category, CategoryDTO>();
            });
            var mapper = new Mapper(config);
            var CovertedData = mapper.Map<List<CategoryDTO>>(data);
            return View(CovertedData);
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(CategoryDTO category)
        {
            if (ModelState.IsValid){
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<CategoryDTO, Category>();
                });
                var mapper = new Mapper(config);
                var ConvertedData = mapper.Map<Category>(category);
                db.Categorys.Add(ConvertedData);
                TempData["Msg"] = "New Category Added.";
                db.SaveChanges();
                return RedirectToAction("Category");
            }
            return View(category);
        }
        public ActionResult Accept(int id)
        {
            var order = db.Orders.Find(id);
            var orderProducts = order.OrderProducts;
            foreach (var item in orderProducts)
            {
                item.Product.Qty -= item.Qty;
                
            }
            order.Status = "Processing";
            db.SaveChanges();
            TempData["Msg"] = "Order Id " + id + " processing";
            return RedirectToAction("Index","Order");
        }
        public ActionResult Decline(int id) 
        {
            var order = db.Orders.Find(id);
            order.Status = "Declined";
            db.SaveChanges();
            TempData["Msg"] = "Order Id " + id + " declined";
            return RedirectToAction("Index", "Order");
        }
        
    }
}