using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureProject.Common.Services
{
    public class ProductHistoryService
    {
        FurnitureDBContext context;

        public ProductHistoryService()
        {
            context = new FurnitureDBContext();
        }

        public OperationResult<CustomerProducts> GetProductsForCustomer(string searchString)
        {
            if (searchString.IsEmpty())
            {
                return new OperationResult<CustomerProducts>("Please specifiy search criteria");
            }

            var customers = context.Customers.Where(c => c.MOL.Contains(searchString))
                                             .ToList();

            if (customers != null && customers.Count() > 1)
            {
                return new OperationResult<CustomerProducts>("More than one user with this name exists. Please specify different search criteria");
            }


            var resultProducts = context.Orders.Where(o => o.Customer.MOL.Contains(searchString))
                                               .Select(o => new
                                               {
                                                   OrderDate = o.Date,
                                                   Products = o.ProductOrders.Select(po => new { po.Quantity, po.Product })
                                               })
                                                .SelectMany(p => p.Products.Select(pp => new ProductWithQuantity()
                                                {
                                                    Product = pp.Product,
                                                    Quantity = pp.Quantity,
                                                    Date = p.OrderDate
                                                }))
                                                .ToList();
            if (resultProducts == null)
            {
                return new OperationResult<CustomerProducts>("The customer has no orders");
            }

            return new OperationResult<CustomerProducts>(new CustomerProducts()
            {
                Customer = customers.FirstOrDefault(),
                Products = resultProducts
            });
        }

        public class OperationResult
        {
            public bool Success { get; set; }
            public string Error { get; set; }

            public OperationResult()
            {
                Success = true;
            }

            public OperationResult(string error)
            {
                Success = false;
                Error = error;
            }
        }

        public class OperationResult<T> : OperationResult
        {
            public T Data { get; set; }

            public OperationResult() : base()
            {
                
            }

            public OperationResult(string error) : base(error)
            {

            }

            public OperationResult(T data)
            {
                Data = data;
                Success = true;
            }
        }
    }

    public static class Extensions
    {
        public static bool IsEmpty(this string str)
        {
            return String.IsNullOrEmpty(str);
        }
       
    }
}