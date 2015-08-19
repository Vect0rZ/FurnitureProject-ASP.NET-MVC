using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FurnitureProject.Common;


namespace FurnitureProject.Models
{
    public class CustomerIndexVM
    {
        public List<Customer> Customers { get; set; }

        public int RowsPerPage { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfPages { get; set; }
    }
}