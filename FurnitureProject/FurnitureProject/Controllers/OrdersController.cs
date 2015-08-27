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
            if(model.OrderID.HasValue == false)
            {
                model.Message = "Please enter valid id";
            }

            if(model.OrderID != null)
            {
                model.OrderAndProducts = OrderService.GetOrderAndProducts(model.OrderID);

                if(model.OrderAndProducts.Products.Count == 0)
                {
                    model.Message = "Could not find such order.";
                }
            }

            return View(model);
        }

    }
}
