using CustomerWebApi.Data;
using Microsoft.AspNetCore.Mvc;
using CustomerWebApi.Model;

namespace CustomerWebApi.Manager
{
    public interface ICustomerManager
    {
        public JsonResult GetCustomerById(int customerId);
        public JsonResult GetCustomerList();
        public JsonResult GetCustomerByFirstName(string firstName);
        public JsonResult SaveCustomer(Customer customer);
    }
}
