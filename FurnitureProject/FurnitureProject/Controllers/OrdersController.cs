using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;

using FurnitureProject.Common;
using FurnitureProject.Models;
using FurnitureProject.Common.Services.OrderService;

namespace FurnitureProject.Controllers
{
    public class OrdersController : BaseController
    {
        public FurnitureDBContext context = new FurnitureDBContext();
        // GET: Orders
        public ActionResult Index(OrdersIndexVM model)
        {
            model.CustomerName = CustomerService.GetCustomerByID(model.CustomerID).MOL;
            if(CustomerService.CustomerExists(model.CustomerID) == false)
            {
                return RedirectToAction("Index", "Home");
            }

            //model.Orders = OrderService.GetOrdersWithTotalPrice(OrderService.GetAllCustomerOrders(model.CustomerID).ToList());
            //var query = from order in context.Orders
            //            where order.CustomerID == model.CustomerID
            //            select new { Order = order, Price = order.ProductOrders.Sum(po => po.Quantity * po.Product.Price) };  
            //var list = query.ToList();

            model.Orders = OrderService.GetOrdersWithTotalPriceProper(model.CustomerID);

            return View(model);
        }

        public ActionResult Products(OrdersProductsVM model)
        {
            if(model.OrderID < 0)
            {
                return View();
            }

            model.ProductOrders = OrderService.GetAllProductOrders(model.OrderID).ToList();
            model.Products = OrderService.GetAllProducts(model.OrderID).ToList();

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
