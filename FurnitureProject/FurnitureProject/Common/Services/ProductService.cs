using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FurnitureProject.Common.Helpers;
using FurnitureProject.Common.Managers;
using FurnitureProject.Models;

namespace FurnitureProject.Common.Services
{
    /// <summary>
    /// A service that manages products.
    /// </summary>
    public class ProductService
    {
        #region Fields

        private FurnitureDBContext context;

        #endregion

        #region Public Methods

        public ProductService()
        {
            context = new FurnitureDBContext();
        }

        /// <summary>
        /// Retrieves all products from the database./>
        /// </summary>
        /// <returns>Returns collection of <see cref="Product"/> objects.</returns>
        public IQueryable<Product> GetAll()
        {
            return context.Products;
        }

        public Product GetByID(int productId)
        {
            return GetAll().FirstOrDefault(p => p.ID == productId);
        }
        public IQueryable<Product> GetAllById(int productID)
        {
            var resultProduts = GetAll().Where(p => p.ID == productID);

            return resultProduts;
        }

        public IQueryable<Product> GetProductsWithName(string name)
        {
            var resultProducts = context.Products.Where(p => p.Name.Contains(name));

            return resultProducts;
        }

        public IQueryable<ProductOrder> GetProductOrders(int orderID)
        {
            return context.ProductOrders.Where(po => po.OrderID == orderID);
        }

        public IQueryable<Product> GetProductsFiltered(string searchString, float? price, bool isLess)
        {
            var resultQuery = GetAll();

            resultQuery = resultQuery.Where(p => String.IsNullOrEmpty(searchString) == true ||
                                                 p.Name.Contains(searchString))
                                     .Where(p => (price == null || price.Value <= 0) 
                                                || (isLess == true && p.Price < price)
                                                || (isLess == false && p.Price >= price));

            #region we ceep this commented out instead of deleting it because... (ML: replace the text of the region with some reason)
            //if(String.IsNullOrEmpty(searchString) == false)
            //{
            //    resultQuery = resultQuery.Where(p => p.Name.Contains(searchString));
            //}

            //if (price != null && price.Value > 0 && isLess == false)
            //{
            //    resultQuery = resultQuery.Where(p => p.Price > price);
            //}

            //if(price != null && price.Value > 0 && isLess == true)
            //{
            //    resultQuery = resultQuery.Where(p => p.Price < price);
            //}
            #endregion

            return resultQuery;
        }

        public List<ProductInfo> GetProductsInfo(string searchString, float? price, bool isLess)
        {
            var resultQuery = GetProductsFiltered(searchString, price, isLess);
            var lastMonth = DateTime.Now.AddYears(-10); // AddMonths(-1)

            return resultQuery.Select(p => new { Product = p,
                                                 Quantity = p.ProductOrders.Where(po => po.Order.Date > lastMonth)
                                                                           .Sum(po => po.Quantity)})
                               .Select(pi => new ProductInfo()
                                        {
                                            Product = pi.Product,
                                            SoldLastMonth = pi.Quantity
                                        })
                               .ToList();
        }

        public List<Product> GetProductsWithPrice(List<Product> toUpdate, float price, bool isLess)
        {
            var resultProducts = toUpdate.AsQueryable();

            if(isLess)
            {
                resultProducts = resultProducts.Where(p => p.Price < price);
            }
            else
            {
                resultProducts = resultProducts.Where(p => p.Price > price);
            }

            return resultProducts.ToList();
        }

        public List<ProductWithQuantity> GetAllProducts(int? orderID = null)
        {
            return context.ProductOrders.Where(po => orderID == null || po.OrderID == orderID.Value)
                                                .Select(o => new { Product = o.Product, Quantity = o.Quantity, Order = o.Order })
                                                .ToList()
                                                .Select(r => new ProductWithQuantity()
                                                {
                                                    Product = r.Product
                                                    ,Quantity = r.Quantity
                                                    ,TotalPrice = r.Product.Price * r.Quantity
                                                })
                                                .ToList();
        }

        public EntityTaskResult<Product> AddProduct(AddProductViewModel product)
        {
            Product resultProduct = new Product()
            {
                Barcode = product.Barcode,
                Name = product.Name,
                Description = product.Description,
                Weight = product.Weight,
                Price = product.Price
            };

            if(ProductManager.ValidateProduct(resultProduct) == false)
            {
                return new EntityTaskResult<Product>().SetErrorMessage("Not a valid product")
                                                      .SetSuccess(false);
            }

            if(CheckForDuplicateBarcode(resultProduct) == true)
            {
                return new EntityTaskResult<Product>().SetErrorMessage("There is already a product with this barcode.")
                                                      .SetSuccess(false);
            }
            context.Products.Add(resultProduct);

            if(context.SaveChanges() > 0)
            {
                return new EntityTaskResult<Product>().SetData(resultProduct).SetSuccess(true);
            }

            return new EntityTaskResult<Product>().SetErrorMessage("Something went wrong")
                                                  .SetSuccess(false);
        }

        public bool DeleteProduct(int? productId)
        {
            bool result = false;

            Product product = context.Products.FirstOrDefault(p => p.ID == productId);
            context.Products.Remove(product);

            int changes = context.SaveChanges();

            if(changes > 0)
            {
                result = true;
            }

            return result;
        }

        public AddProductViewModel GetProductViewModel(int prodId, string errorMessage = null)
        {
            AddProductViewModel result = null;
            Product product = GetByID(prodId);

            if(product != null)
            {
                result = new AddProductViewModel(product, errorMessage);
            }

            return result;
        }

        public EntityTaskResult<Product> EditProduct(AddProductViewModel model)
        {
            Product product = GetByID(model.ProductID.Value);

            if(product == null)
            {
                return new EntityTaskResult<Product>().SetErrorMessage("Cannot find such product")
                                                      .SetSuccess(false);
            }

            product.Barcode = model.Barcode;
            product.Name = model.Name;
            product.Description = model.Description;
            product.Weight = model.Weight;
            product.Price = model.Price;

            if(CheckForDuplicateBarcode(product, product.ID) == true)
            {
                return new EntityTaskResult<Product>().SetErrorMessage("There is already a product with such barcode.")
                                                      .SetSuccess(false);
            }

            context.Products.Attach(product);
            context.Entry(product).State = System.Data.Entity.EntityState.Modified;

            int changes = context.SaveChanges();

            if(changes > 0)
            {
                return new EntityTaskResult<Product>().SetData(product)
                                                      .SetSuccess(true);
            }

            return new EntityTaskResult<Product>().SetErrorMessage("Something went wrong.")
                                                  .SetSuccess(false);
        }

        #endregion

        #region private methods

        private bool CheckForDuplicateBarcode(Product product, int? excludeId = null)
        {
            bool result = false;

            Product existingProduct = context.Products.FirstOrDefault(p => p.Barcode == product.Barcode);

            if(existingProduct != null)
            {
                result = true;
            }

            if (existingProduct != null && excludeId != null && existingProduct.ID == excludeId)
            {
                result = false;
            }

            return result;
        }
        #endregion
    }
}