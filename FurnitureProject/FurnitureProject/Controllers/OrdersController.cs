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

        public ActionResult Index(OrdersIndexVM model)
        {
            var customer = CustomerService.GetCustomerByID(model.CustomerID);

            if(customer == null)
            {
                return RedirectToAction("Index", "Home");
            }

            model.CustomerName = customer.MOL;
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
    }
}
