using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CustomerManagementUI.Models;
using System.Text;

namespace CustomerManagementUI.Controllers
{
    public class CustomerViewController : Controller
    {
        private readonly HttpClient _httpClient;

        public CustomerViewController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        // GET: Customer/Index
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/customer/GetCustomerList"); 
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(jsonString);
                var customers = customerResponse.CustomerList; // Extract the list from the response

                return View(customers);
            }

            return View(new List<Customer>());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(customer);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                try
                {
                    var response = await _httpClient.PostAsync("api/customer/SaveCustomer", contentString);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError("", $"Error saving customer: {errorContent}");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Exception occurred: {ex.Message}");
                }
            }

            // If we reach this point, something went wrong
            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/customer/GetCustomerById?customerId={id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(jsonString);

                if (customerResponse.Result && customerResponse.Customer != null)
                {
                    return View(customerResponse.Customer); // Pass the single customer to the view
                }
            }

            // Handle error if needed
            ModelState.AddModelError("", "Error retrieving customer details.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerManagementUI.Models.Customer customer)
        {
            if (ModelState.IsValid)
            {
                // Serialize the customer object to JSON
                var jsonContent = JsonConvert.SerializeObject(customer);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                try
                {
                    // Call the SaveCustomer API method (which could be an update method)
                    var response = await _httpClient.PostAsync("api/customer/SaveCustomer", contentString);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError("", $"Error saving customer: {errorContent}");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Exception occurred: {ex.Message}");
                }
            }

            // If we reach this point, something went wrong
            return View(customer);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Call the API to get the customer details for confirmation
                var response = await _httpClient.GetAsync($"api/customer/GetCustomerById?customerId={id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(jsonString);

                    // Assuming customerResponse.Customer contains the customer details
                    var customer = customerResponse.Customer;

                    return View(customer);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Error retrieving customer: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Exception occurred: {ex.Message}");
            }

            // If we reach this point, something went wrong
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.PostAsync($"api/customer/DeleteCustomer?customerId={id}", null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error deleting customer.");
            return RedirectToAction("Index");
        }



    }
}
