using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FurnitureProject.Common;

using FurnitureProject.Models;

namespace FurnitureProject.Controllers
{
    public class ProductsController : BaseController
    {
        // GET: Products
        public ActionResult Index(ProductsIndexVM model)
        {
            //if(String.IsNullOrEmpty(model.SearchString) == true)
            //{
            //    model.SearchString = "";
            //}
            
            //model.Products = ProductService.GetProductsWithName(model.SearchString).ToList();
            //if (model.Price > 0)
            //{
            //    model.Products = ProductService.GetProductsWithPrice(model.Products, model.Price, model.isLessThan);
            //}

            var query = ProductService.GetProductsFiltered(model.SearchString, model.Price, model.isLessThan);

            if(model.OrderID != null)
            {
                query = OrderService.GetAllProductOrders((int)model.OrderID).Select(po => po.Product);
            }
            
            model.Products = query.ToList();

            return View(model);
        }

    }
}
