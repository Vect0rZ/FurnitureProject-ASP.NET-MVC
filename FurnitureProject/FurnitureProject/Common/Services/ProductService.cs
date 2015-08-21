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
        public IQueryable<Product> GetProductsFiltered(string searchString, float? price, bool isLess)
        {
            var resultQuery = GetAll();

            if(String.IsNullOrEmpty(searchString) == false)
            {
                resultQuery = resultQuery.Where(p => p.Name.Contains(searchString));
            }

            if(price != null && price.Value > 0)
            {
                resultQuery = resultQuery.Where(p => p.Price < price);
            }

            return resultQuery;
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

        public List<ProductWithSold> GetAllProductsWithSold(int? orderId)
        {
            return context.ProductOrders.Where(po => po.OrderID == orderId)
                    .Select(po => new
                    {
                        Product = po.Product
                        ,
                        Order = po.Order
                        ,
                        Quantity = po.Quantity
                        ,
                        soldLastMonth = po.Order.ProductOrders.Sum(po1 => po1.Quantity)
                    })
                    .Where(r => r.Order.Date > new DateTime(2000, 1, 1))
                    .ToList()
                    .Select(r => new ProductWithSold() { Product = r.Product, soldLastMonth = r.soldLastMonth })
                    .ToList();
        }
    }
}