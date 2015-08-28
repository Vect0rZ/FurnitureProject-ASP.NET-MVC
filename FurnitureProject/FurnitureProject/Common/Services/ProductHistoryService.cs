using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureProject.Common.Services
{
    /// <summary>
    /// A service that manages history of sold products.
    /// </summary>
    public class ProductHistoryService
    {
        #region Fields

        private FurnitureDBContext context;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates new instance of the Product History service, using the real furniture DB context.
        /// </summary>
        public ProductHistoryService()
        {
            context = new FurnitureDBContext();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves list of customer products by given search criteria.
        /// </summary>
        /// <param name="searchString">Search filter criteria.</param>
        /// <returns>Returns OperationResult containing <see cref="CustomerProducts"/> if any. </returns>
        public OperationResult<CustomerProducts> GetProductsForCustomer(string searchString)
        {
            
            if (searchString.IsEmpty())
            {
                return new OperationResult<CustomerProducts>("Please specifiy search criteria", new CustomerProducts());
            }

            var customers = context.Customers.Where(c => c.MOL.Contains(searchString))
                                             .ToList();
            if(customers.Count() <= 0)
            {
                return new OperationResult<CustomerProducts>("No such user found", new CustomerProducts());
            }
            if (customers != null && customers.Count() > 1)
            {
                return new OperationResult<CustomerProducts>("More than one user with this name exists. Please specify different search criteria", new CustomerProducts());
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
                return new OperationResult<CustomerProducts>("The customer has no orders", new CustomerProducts());
            }

            return new OperationResult<CustomerProducts>(new CustomerProducts()
            {
                Customer = customers.FirstOrDefault(),
                Products = resultProducts
            });
        }

        #endregion

        #region OperationResult helpers
        //ML: it's better to put this class separately, maybe in "Helper" folder

        public class OperationResult
        {
            public bool Success { get; set; }
            public string Error { get; set; }

            public OperationResult()
            {
                Success = true;
            }

            //ML: it doesn't becomes clear, that this constructor should be used in case of an error only. 
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

            public OperationResult(string error, T data) : base(error)
            {
                Data = data;
            }

            public OperationResult(T data)
            {
                Data = data;
                Success = true;
            }
        }

        #endregion
    }

    public static class Extensions
    {
        public static bool IsEmpty(this string str)
        {
            //ML: better use IsNullOrWhitespace, in this case you don't need to trim it. Otherwise the user will enter one space and this function will return false.
            return String.IsNullOrWhiteSpace(str);
        }
       
    }
}