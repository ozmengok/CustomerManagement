namespace CustomerManagementUI.Models
{
    public class CustomerResponse
    {
        public bool Result { get; set; }
        public List<Customer> CustomerList { get; set; }
        public Customer Customer { get; set; } // Added this line
    }
}
