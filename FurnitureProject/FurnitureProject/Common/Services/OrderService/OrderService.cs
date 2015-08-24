using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

using FurnitureProject.Common.Services.OrderService;

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
        public IQueryable<Order> GetAll()
        {
            return context.Orders;
        }

        public Order GetWithId(int id)
        {
            Order resultOrder = GetAll().FirstOrDefault(p => p.ID == id);

            return resultOrder;
        }

        public OrderWithTotalPrice GetWithIdAndTotalPrice(int id)
        {
            var resultOrder = GetAll().Where(o => o.ID == id)
                                .Select(p => new
                                {
                                    Order = p
                                    ,
                                    TotalPrice = p.ProductOrders.Sum(po => po.Quantity * po.Product.Price)
                                    ,
                                    Customer = p.Customer
                                    ,
                                    Products = p.ProductOrders.Select(po => po.Product)
                                })
                                .ToList()
                                .Select(r => new OrderWithTotalPrice()
                                {
                                    Order = r.Order
                                    ,
                                    TotalPrice = r.TotalPrice
                                    ,
                                    Customer = r.Customer
                                }).FirstOrDefault();
            return resultOrder;
        }

        public OrderAndProducts GetOrderAndProducts(int? id)
        {
            var resultOrder = GetAll().Where(o => o.ID == id)
                                .Select(o => new
                                {
                                    Order = o,
                                    Customer = o.Customer,
                                    Products = o.ProductOrders.Select(po => new { po.Product, po.Quantity })
                                })
                                .ToList()
                                .Select(r => new OrderAndProducts()
                                {

                                    Order = new OrderWithTotalPrice()
                                            {
                                                Order = r.Order,
                                                Customer = r.Customer,
                                                TotalPrice = r.Products.Sum(p => p.Product.Price * p.Quantity)
                                            },
                                    Products = r.Products.Select(pwq => new ProductWithQuantity()
                                                                {
                                                                    Product = pwq.Product,
                                                                    Quantity = pwq.Quantity,
                                                                    TotalPrice = pwq.Product.Price * pwq.Quantity
                                                                })
                                                         .ToList()
                                })
                                .FirstOrDefault();

            return resultOrder;
        }

        public IQueryable<Order> GetAllCustomerOrders(int customerID)
        {
            var resultQuery = context.Orders.Where(o => o.CustomerID == customerID);

            return resultQuery;
        }

        public IQueryable<ProductOrder> GetAllProductOrders()
        {
            var resultQuery = context.ProductOrders;

            return resultQuery;
        }

        public IQueryable<ProductOrder> GetAllProductOrders(int id)
        {
            var resultQuery = GetAllProductOrders().Where(po => po.OrderID == id);

            return resultQuery;
        }

        public Dictionary<Order, float> GetOrdersWithTotalPrice(List<Order> orders)
        {
            var resultDictionary = new Dictionary<Order, float>();

            foreach (Order o in orders)
            {
                float priceSum = o.ProductOrders.Sum(po => po.Quantity * po.Product.Price);
                resultDictionary.Add(o, priceSum);
            }

            return resultDictionary;
        }

        public List<OrderWithTotalPrice> GetOrdersWithTotalPriceProper(int? customerId)
        {
            List<OrderWithTotalPrice> result;
            //TRANSLATION:
            /*
             * IF Customer Id is null then continue with the query, 
             * if not FILTER o.CustemerID == customerID and continue onwards
             * */
            result = context.Orders.Where(o => customerId == null || o.CustomerID == customerId)
                                   .Select(o => new { Order = o, Price = o.ProductOrders.Sum(po => po.Quantity * po.Product.Price) })
                                   .ToList()
                                   .Select(r => new OrderWithTotalPrice() { Order = r.Order, TotalPrice = r.Price })
                                   .ToList();

            return result;
        }

        public OrderWithTotalPrice GetOrderWithTotalPrice(int customerId, int? orderId)
        {
            return GetOrdersWithTotalPriceProper(customerId).FirstOrDefault(o => o.Order.ID == orderId.Value);
        }
    }
}