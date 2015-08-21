using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureProject.Common.Services
{
    public class ProductWithQuantity
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}