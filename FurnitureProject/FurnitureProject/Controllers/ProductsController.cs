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

        [Authorize]
        public ActionResult AddProduct(AddProductViewModel product)
        {
            if(ModelState.IsValid == false)
            {
                return View(product);
            }

            var result = ProductService.AddProduct(product);

            if(result.Success == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                product.ErrorMessage = result.ErrorMessage;
            }

            return View(product);
        }

        public ActionResult EditProduct(int? prodId, AddProductViewModel model)
        {

            if(prodId.HasValue == true)
            {
                model = ProductService.GetProductViewModel(prodId.Value);
            }

            if(ModelState.IsValid == false)
            {
                return View(model);
            }

            if(prodId.HasValue == false)
            {
                var result = ProductService.EditProduct(model);
                if(result.Success == true)
                {
                    return RedirectToAction("Index");
                }

                model.ErrorMessage = result.ErrorMessage;
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteProduct(int? productId)
        {
            if(ProductService.DeleteProduct(productId) == true)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult CustomerProducts()
        {
            return View();
        }

    }
}
