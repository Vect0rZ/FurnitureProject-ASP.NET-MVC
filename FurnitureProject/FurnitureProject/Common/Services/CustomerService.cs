using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureProject.Common.Services
{
    public class CustomerService
    {
        private FurnitureDBContext context;

        public CustomerService()
        {
            context = new FurnitureDBContext();
        }

        public int ValidatePageNumber(int pageNumber)
        {
            if(pageNumber < 0)
            {
                return 0;
            }

            return pageNumber;
        }

        public int ValidateRowsPerPage(int rowsPerPage)
        {
            if(rowsPerPage <= 0 || rowsPerPage % 25 != 0)
            {
                return 25;
            }

            return rowsPerPage;
        }

        public int GetNumberOfPages(int rowsPerPage)
        {
            return context.Customers.Count() / rowsPerPage;
        }

        //Should probably be in repository
        public List<Customer> GetAllOnPage(int pageNumber, int rowsPerPage)
        {
            var customers = context.Customers.OrderBy(c => c.MOL);
            return customers.Skip(pageNumber * rowsPerPage).Take(rowsPerPage).ToList();
        }
    }
}