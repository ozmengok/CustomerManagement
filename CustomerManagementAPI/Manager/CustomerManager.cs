using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using CustomerWebApi.Data;
using CustomerWebApi.Model;

namespace CustomerWebApi.Manager
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public JsonResult GetCustomerById(int customerId)
        {           
            try
            {
                var customer = _customerRepository.GetCustomerById(customerId);
                if(customer == null || customer.Id == 0)
                {
                    return new JsonResult(new { Result = false, Error = "Customer not found" });
                }

                return new JsonResult(new { Result = true, customer } );
            }
            catch (Exception ex)
            {
                return new JsonResult(HttpStatusCode.ServiceUnavailable, ex.ToString());
            }
        }

        public JsonResult GetCustomerList()
        {
            try
            {
                var customerList = _customerRepository.GetListOfCustomer();
                return new JsonResult(new { Result = true, customerList });
            }
            catch (Exception ex)
            {
                return new JsonResult(HttpStatusCode.ServiceUnavailable, ex.ToString());
            }
            
        }

        public JsonResult GetCustomerByFirstName(string firstName)
        {
            try
            {
                var customerList = _customerRepository.GetCustomerByFirstName(firstName);
                return new JsonResult(new { Result = true, customerList });
            }
            catch (Exception ex)
            {
                return new JsonResult(HttpStatusCode.ServiceUnavailable, ex.ToString());
            }
               
        }


        public JsonResult SaveCustomer(Customer customer)
        {
            try
            {
                if(customer == null)
                {
                    return new JsonResult(new { Result = false, Error = "Customer is not valid" });
                }

                if (String.IsNullOrEmpty(customer.Surname))
                {
                    return new JsonResult(new { Result = false, Error = "Surname can not be empty" });

                }
                else if (String.IsNullOrEmpty(customer.Firstname))
                {
                    return new JsonResult(new { Result = false, Error = "Firstname can not be empty" });
                }


                customer = _customerRepository.SaveCustomer(customer);        
                if(customer == null)
                {
                    return new JsonResult(new { Result = false, Error = "Customer is not valid" });
                }

                return new JsonResult(new { Result = true, Customer = customer });
            }
            catch (Exception ex)
            {
                return new JsonResult(HttpStatusCode.ServiceUnavailable, ex.ToString());
            }


                

           

        }








    }
}
