using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureProject.Common.Services
{
    public class ProductService
    {
        private FurnitureDBContext context;

        public ProductService()
        {
            context = new FurnitureDBContext();
        }

        public IQueryable<Product> GetAll()
        {
            return context.Products;
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

        public IQueryable<Product> GetProductsFiltered(string searchString, float price, bool isLess)
        {
            if(String.IsNullOrEmpty(searchString) == true ||
                price <= 0)
            {
                return GetAll();
            }

            var resultProducts = GetAll().Where(p => p.Name.Contains(searchString)).Where(p => p.Price < price);

            return resultProducts;
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
    }
}