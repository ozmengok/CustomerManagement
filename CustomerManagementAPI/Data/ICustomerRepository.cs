using CustomerWebApi.Model;

namespace CustomerWebApi.Data
{
    public interface ICustomerRepository
    {
        public Customer GetCustomerById(int customerId);
        public List<Customer> GetListOfCustomer();
        public List<Customer> GetCustomerByFirstName(string firstName);
        public Customer SaveCustomer(Customer customer);

    }
}
