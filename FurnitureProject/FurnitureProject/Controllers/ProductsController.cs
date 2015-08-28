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


        public ActionResult EditProductGet(AddProductViewModel model)
        {
            if(model.ProductID.HasValue == true)
            {
                model = ProductService.GetProductViewModel(model.ProductID.Value, model.ErrorMessage);
            }

            return View(model);
        }


        public ActionResult EditProductPost(AddProductViewModel model)
        {
            if(ModelState.IsValid == false)
            {
                return RedirectToAction("EditProductGet", model);
            }

            var result = ProductService.EditProduct(model);

            if(result.Success == true)
            {
                return RedirectToAction("Index");
            }

            model.ErrorMessage = result.ErrorMessage;
            
            return RedirectToAction("EditProductGet", model);
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
