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
    public class ProductController : Controller
    {
        PMS_Sm24_AEntities db = new PMS_Sm24_AEntities();
        // GET: Product
        [AdminAccess]
        public ActionResult Index()
        {
            var data = db.Products.ToList();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDTO>();
            });
            var mapper = new Mapper(config);
            var CovertedData = mapper.Map<List<ProductDTO>>(data);
            return View(CovertedData);
        }
        [AdminAccess]
        [HttpGet]
        public ActionResult AddProduct()
        {
            return View(); 
        }
        [AdminAccess]
        [HttpPost]
        public ActionResult AddProduct(ProductDTO product)
        {
            if (ModelState.IsValid) {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ProductDTO, Product>();
                });
                var mapper = new Mapper(config);
                var ConvertedData = mapper.Map<Product>(product);
                db.Products.Add(ConvertedData);
                TempData["Msg"] = "New Product Adeded";
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(product);
        }
        [AdminAccess]
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            var data = db.Products.Find(id);
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDTO>();
            });
            var mapper = new Mapper(config);
            var ConvertedData = mapper.Map<ProductDTO>(data);
            return View(ConvertedData);
        }
        [HttpPost]
        public ActionResult EditProduct(ProductDTO product)
        {
            var data = db.Products.Find(product.Id);
            if (ModelState.IsValid)
            {
                data.Name =product.Name;
                data.Price =product.Price;
                data.Qty =product.Qty;
                data.CId = product.CId;
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ProductDTO, Product>();
                });
                var mapper = new Mapper(config);
                var ConvertedData = mapper.Map<Product>(data);
                
                TempData["Msg"] = "Product Data Updated";
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(product);
        }
        [CustomerAccess]
        public ActionResult ShowProduct()
        {
            var data = db.Products.ToList();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDTO>();
            });
            var mapper = new Mapper(config);
            var CovertedData = mapper.Map<List<ProductDTO>>(data);
            return View(CovertedData);
        }
    }
}