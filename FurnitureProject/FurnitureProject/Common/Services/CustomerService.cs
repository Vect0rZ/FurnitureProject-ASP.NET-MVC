using FurnitureProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FurnitureProject.Common.Managers;
using FurnitureProject.Common.Helpers;

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

        public bool CustomerExists(int customerID)
        {
            var query = context.Customers.Where(c => c.ID == customerID);

            if (query.Count() > 0)
            {
                return true;
            }

            return false;
        }

        public int ValidateRowsPerPage(int rowsPerPage)
        {
            if(rowsPerPage <= 0 || rowsPerPage % 25 != 0)
            {
                return 25;
            }

            return rowsPerPage;
        }

        public Customer GetCustomerByID(int? customerId)
        {
            Customer result = null; ;

            if(customerId != null)
            {
                result = context.Customers.Find(customerId);
            }

            return result;
        }

        public int GetNumberOfPages(int rowsPerPage)
        {
            return context.Customers.Count() / rowsPerPage;
        }

        public List<Customer> GetAllOnPage(int pageNumber, int rowsPerPage)
        {
            var customers = context.Customers.OrderBy(c => c.MOL);
            return customers.Skip(pageNumber * rowsPerPage).Take(rowsPerPage).ToList();
        }

        public EntityTaskResult<Customer> AddCustomer(AddCustomerViewModel customer)
        {

            Customer resultCustomer = new Customer()
            {
                Bulstat = customer.Bulstat,
                Name = customer.Name,
                MOL = customer.MOL,
                Address = customer.Address
            };

            if(CustomerManager.ValidateCustomer(resultCustomer) == false)
            {
                return new EntityTaskResult<Customer>().SetErrorMessage("Not a valid customer")
                                                       .SetSuccess(false);
            }
            if (CheckForDuplicateBulstat(resultCustomer) == true)
            {
                return new EntityTaskResult<Customer>().SetErrorMessage("There is allready a customer with such Bulstat")
                                                       .SetSuccess(false);
            }
            context.Customers.Add(resultCustomer);
            
            if(context.SaveChanges() > 0)
            {
                return new EntityTaskResult<Customer>().SetData(resultCustomer)
                                                       .SetSuccess(true);
            }

            return new EntityTaskResult<Customer>().SetErrorMessage("Something went wrong.").SetSuccess(false);
        }

        public bool DeleteCustomer(int? customerId)
        {
            bool result = false;

            Customer customer = GetCustomerByID(customerId);

            context.Customers.Remove(customer);

            int changes = context.SaveChanges();

            if(changes > 0)
            {
                result = true;
            }

            return result;
        }
        #region private methods

        private bool CheckForDuplicateBulstat(Customer customer)
        {
            bool result = false;
            Customer existingCustomer = context.Customers.FirstOrDefault(c => c.Bulstat == customer.Bulstat);

            if(existingCustomer != null)
            {
                result = true;
            }

            return result;
        }

        #endregion
    }
}