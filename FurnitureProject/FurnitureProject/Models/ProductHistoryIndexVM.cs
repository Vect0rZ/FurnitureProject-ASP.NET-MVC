using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FurnitureProject.Common;
using FurnitureProject.Common.Services;

namespace FurnitureProject.Models
{
    public class ProductHistoryIndexVM
    {
        public CustomerProducts CustomerProducts { get; set; }
        public string SearchString { get; set; }
        public String ErrorMessage { get; set; }
        public ProductHistoryIndexVM()
        {
            SearchString = string.Empty;
            CustomerProducts = new CustomerProducts();
        }
    }
}