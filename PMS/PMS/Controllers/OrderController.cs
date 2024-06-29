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
    
    public class OrderController : Controller
    {
        PMS_Sm24_AEntities db = new PMS_Sm24_AEntities();
        // GET: Order
        [AdminAccess]
        public ActionResult Index()
        {
            var data = db.Orders.ToList();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Order, OrderDTO>();
            });
            var mapper = new Mapper(config);
            var CovertedData = mapper.Map<List<OrderDTO>>(data);
            return View(CovertedData);
        }
        [Logged]
        public ActionResult Details(int id) { 
            var data = (from OrderData in db.OrderProducts
                        where OrderData.OId == id
                        select OrderData).ToList();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<OrderProduct, OrderProductDTO>();
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<Order, OrderDTO>();

            });
            var mapper = new Mapper(config);
            var pd = mapper.Map<List<OrderProductDTO>>(data);
            return View(pd);
        }
        [CustomerAccess]
        public ActionResult AddToCart(int id)
        {
            var product = db.Products.Find(id); 
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDTO>();

            });
            var mapper = new Mapper(config);
            var pr = mapper.Map<ProductDTO>(product);
            pr.Qty = 1;
            if (Session["cart"] == null)
            {
                var cart = new List<ProductDTO>();
                cart.Add(pr);
                Session["cart"] = cart;
            }
            else
            {
                var cart = Session["cart"];
                var data = (List<ProductDTO>)cart;
                data.Add(pr);
                Session["cart"] = data;

            }

            TempData["Msg"] = pr.Name + " Added Successfully";
            return RedirectToAction("ShowProduct","Product");
        }
        [CustomerAccess]
        public ActionResult Cart()
        {
            var cart = Session["cart"];
            if (cart == null)
            {
                TempData["Msg"] = "Cart Empty";
                return RedirectToAction("Index");
            }
            var data = (List<ProductDTO>)cart;
            return View(data);


        }
        [CustomerAccess]
        [HttpPost]
        public ActionResult PlaceOrder(decimal Total)
        {
            var order = new Order();
            order.OderDate = DateTime.Now;
            order.Status = "Ordered";
            order.TotalAmount = Total;
            order.UserId = ((User)Session["user"]).Id;
            db.Orders.Add(order);
            db.SaveChanges();
            var cart = (List<ProductDTO>)Session["cart"];
            foreach (var p in cart)
            {
                var op = new OrderProduct();
                op.UnitPrice = p.Price;
                op.Qty = p.Qty;
                op.PId = p.Id;
                op.OId = order.Id;
                db.OrderProducts.Add(op);
            }
            db.SaveChanges();

            Session["cart"] = null;
            TempData["Msg"] = "Order Placed Successfully";
            return RedirectToAction("ShowProduct", "Product");

        }
        [CustomerAccess]
        public ActionResult Orders()
        {
            var user=((User)Session["user"]).Id;
            var data = (from userdata in db.Orders
                        where userdata.UserId.Equals(user)
                        select userdata).ToList();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Order, OrderDTO>();
            });
            var mapper = new Mapper(config);
            var CovertedData = mapper.Map<List<OrderDTO>>(data);
            return View(CovertedData);
        }

    }
    
}