using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureProject.Common.Services
{
    public class ProductInfo
    {
        public Product Product { get; set; }
        public int? SoldLastMonth { get; set; }
    }
}