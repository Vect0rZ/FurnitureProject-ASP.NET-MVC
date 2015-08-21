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

        public ActionResult Index(ProductsIndexVM model)
        {
            IQueryable<Product> query = ProductService.GetProductsFiltered(model.SearchString, model.Price, model.IsLessThan);

            if(model.OrderID.HasValue)
            {
                query = OrderService.GetAllProductOrders(model.OrderID.Value).Select(po => po.Product);
            }
            
            model.Products = query.ToList();

            return View(model);
        }

    }
}
