using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace FurnitureProject.Common.Services.OrderService
{
    public class OrderService
    {
        private FurnitureDBContext context;

        public OrderService()
        {
            context = new FurnitureDBContext();
            context.Database.Log = (s) => Debug.WriteLine(s);
        }

        public IQueryable<Product> GetAllProducts()
        {
            return context.Products;
        }

        public IQueryable<Order> GetAll()
        {
            return context.Orders;
        }

        public Order GetWithId(int id)
        {
            Order resultOrder = GetAll().FirstOrDefault(p => p.ID == id);

            return resultOrder;
        }
        public IQueryable<Order> GetAllCustomerOrders(int customerID)
        {
            var resultQuery = context.Orders.Where(o => o.CustomerID == customerID);

            return resultQuery;
        }

        public IQueryable<ProductOrder> GetAllProductOrders(int id)
        {
            var resultQuery = context.ProductOrders.Where(po => po.OrderID == id);

            return resultQuery;
        }

        public IQueryable<ProductOrder> GetAllProductOrders()
        {
            var resultQuery = context.ProductOrders;

            return resultQuery;
        }

        public List<ProductWithQuantity> GetAllProducts(int orderID)
        {
            return context.ProductOrders.Where(po => po.OrderID == orderID)
           /*.Select(po => po.Product)*/.ToList().Select(r => new ProductWithQuantity() { Product = r.Product, Quantity = r.Quantity }).ToList();

            //foreach(ProductOrder po in ordersQuery.ToList())
            //{

            //    //yield return po.Product;
            //    resultProducts.Add(po.Product);
            //}

            //return resultProducts;
        }

        public Dictionary<Order, float> GetOrdersWithTotalPrice(List<Order> orders)
        {
            
            Dictionary<Order, float> resultDictionary = new Dictionary<Order, float>();

            foreach (Order o in orders)
            {
                float priceSum = o.ProductOrders.Sum(po => po.Quantity * po.Product.Price);
                resultDictionary.Add(o, priceSum);
            }

            return resultDictionary;
        }
        
        public List<OrderWithTotalPrice> GetOrdersWithTotalPriceProper(int customerId)
        {
            List<OrderWithTotalPrice> result =
                context.Orders.Where(o => o.CustomerID == customerId)
                                   .Select(o => new { Order = o, Price = o.ProductOrders.Sum(po => po.Quantity * po.Product.Price) })
                                   .ToList()
                                   .Select(r => new OrderWithTotalPrice() { Order = r.Order, TotalPrice = r.Price})
                                   .ToList();

            return result;
        }

        public float TotalPrice(List<Product> products)
        {
            float sumResult = 0;

            foreach(Product p in products)
            {
                sumResult += p.Price;
            }

            return sumResult;
        }
    }
}