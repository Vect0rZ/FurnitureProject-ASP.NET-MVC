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
using FurnitureProject.Common.Services;

namespace FurnitureProject.Controllers
{
    public class ProductsController : BaseController
    {

        public ActionResult Index(ProductsIndexVM model)
        {
            IQueryable<Product> query = ProductService.GetProductsFiltered(model.SearchString, model.Price, model.IsLessThan);
            List<ProductWithSold> query2 = null;

            if(model.OrderID.HasValue)
            {
                query2 = ProductService.GetAllProductsWithSold(model.OrderID);               
            }

            model.Products = query2;

            return View(model);
        }

    }
}
