using PMS.Auth;
using PMS.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Controllers
{
    [CustomerAccess]
    public class CustomerController : Controller
    {
        PMS_Sm24_AEntities db = new PMS_Sm24_AEntities();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CancelOrder(int id)
        {
            var order = db.Orders.Find(id);
            order.Status = "Canceled";
            db.SaveChanges();
            TempData["Msg"] = "Order Id " + id + " Canceled";
            return RedirectToAction("Orders", "Order");
        }
    }
}