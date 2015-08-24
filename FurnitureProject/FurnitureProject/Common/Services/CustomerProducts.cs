using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureProject.Common.Services
{
    public class CustomerProducts
    {
        public Customer Customer { get; set; }
        public List<ProductWithQuantity> Products { get; set; }
        //public String ErrorMessage { get; set; }
        public CustomerProducts()
        {
            Customer = new Customer();
            Products = new List<ProductWithQuantity>();
        }
    }
}