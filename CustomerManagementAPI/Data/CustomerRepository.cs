

using CustomerWebApi.Model;

namespace CustomerWebApi.Data
{


    public class CustomerRepository : ICustomerRepository
    {
        private readonly DatabaseContext m_Context;


        public CustomerRepository(DatabaseContext context)
        {
            m_Context = context;
        }

        public Customer GetCustomerById(int customerId)
        {
            return m_Context.Customer.FirstOrDefault(x => x.Id == customerId && x.Status == "A");              
        }

        public List<Customer> GetListOfCustomer()
        {
            var customers =  m_Context.Customer.Where(x => x.Status == "A");
            return customers.ToList();
        }

        public List<Customer> GetCustomerByFirstName(string firstName)
        {
            var customers = m_Context.Customer.Where(x => x.Firstname == firstName && x.Status == "A");
            return customers.ToList();

        }
        public Customer  SaveCustomer(Customer customer)
        {   
            if(customer.Id > 0)
            {
                //update
                Customer toUpdateCustomer = m_Context.Customer.FirstOrDefault(x => x.Id == customer.Id);
                if(toUpdateCustomer != null)
                {
                    toUpdateCustomer.Firstname = customer.Firstname;
                    toUpdateCustomer.Surname = customer.Surname;
                    toUpdateCustomer.Status = customer.Status;
                }              

                return toUpdateCustomer;
            }
            else
            {
                //add
                int lastCustomerId = m_Context.Customer.Max(p => p.Id);
                customer.Id = lastCustomerId == null ? 1 : lastCustomerId + 1;

                m_Context.Customer.Add(customer);
            }    

            return customer;
        }

    }

  
}
