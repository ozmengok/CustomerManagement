using Microsoft.AspNetCore.Mvc;
using CustomerWebApi.Manager;
using CustomerWebApi.Data;
using System.Net;
using CustomerWebApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        readonly ICustomerManager _customerManager;


        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        [HttpGet("GetCustomerById")]
        public IActionResult GetCustomerById(int customerId)
        {
            return _customerManager.GetCustomerById(customerId);
        }


        /// <summary>
        /// returns list of the types
        /// </summary>
        [HttpGet("GetCustomerList")]
        public IActionResult GetCustomerList()
        {
            return _customerManager.GetCustomerList();
        }

        [HttpGet("GetCustomerListByName")]
        public IActionResult GetCustomerListByName(string firstName)
        {
            return _customerManager.GetCustomerByFirstName(firstName);
        }
       

        [HttpPost("SaveCustomer")]
        public IActionResult SaveCustomer([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                return _customerManager.SaveCustomer(customer);
               
            }
            else
            {
                return new JsonResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("DeleteCustomer")]
        public IActionResult DeleteCustomer(int customerId)
        {
            if (ModelState.IsValid)
            {
                return _customerManager.DeleteCustomer(customerId);

            }
            else
            {
                return new JsonResult(HttpStatusCode.BadRequest);
            }
        }


    }
}
