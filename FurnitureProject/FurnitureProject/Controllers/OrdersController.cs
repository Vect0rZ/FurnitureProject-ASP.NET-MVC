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
            Customer customer = CustomerService.GetCustomerByID(model.CustomerID);
            model.Orders = OrderService.GetOrdersWithTotalPriceProper(model.CustomerID);

            if(customer == null)
            {
                return RedirectToAction("Index", "Home");
            }

            model.CustomerName = customer.MOL;

            return View(model);
        }


        public ActionResult SearchOrderIndex(OrdersSearchOrderVM model)
        {
            if(model.OrderID != null)
            {
                model.Order.Order = OrderService.GetWithId(model.OrderID.Value);
                if(model.Order.Order != null)
                {
                    model.Customer = model.Order.Order.Customer;
                    model.Products = ProductService.GetAllProducts(model.OrderID).ToList();
                    model.Order.TotalPrice = OrderService.GetOrderWithTotalPrice(model.Customer.ID, model.OrderID).TotalPrice;
                }
            }

            return View(model);
        }

    }
}
