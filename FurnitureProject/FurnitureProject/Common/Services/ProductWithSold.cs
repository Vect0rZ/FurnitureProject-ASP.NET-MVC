using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureProject.Common.Services
{
    public class ProductWithSold
    {
        public Product Product { get; set; }
        public int soldLastMonth { get; set; }
    }
}