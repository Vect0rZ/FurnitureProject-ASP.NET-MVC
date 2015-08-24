using System.Linq;
using System.Web.Mvc;

using FurnitureProject.Common;

using FurnitureProject.Models;

namespace FurnitureProject.Controllers
{
    public class ProductsController : BaseController
    {

        public ActionResult Index(ProductsIndexVM model)
        {
            var products = ProductService.GetProductsInfo(model.SearchString, model.Price, model.IsLessThan);

            model.Products = products;

            return View(model);
        }

        public ActionResult CustomerProducts()
        {
            return View();
        }

    }
}
