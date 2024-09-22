

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
            return m_Context.Customer.FirstOrDefault(x => x.Id == customerId);              
        }

        public List<Customer> GetListOfCustomer()
        {
            return m_Context.Customer.ToList();
        }

        public List<Customer> GetCustomerByFirstName(string firstName)
        {
            return m_Context.Customer.Where(x => x.Firstname == firstName).ToList();

        }
        public Customer  SaveCustomer(Customer customer)
        {   
            if(customer.Id > 0)
            {
                //update
                Customer toUpdateCustomer = m_Context.Customer.FirstOrDefault(x => x.Id == customer.Id);
                if(toUpdateCustomer == null)
                {
                    return null;
                }
                toUpdateCustomer.Firstname  = customer.Firstname;
                toUpdateCustomer.Surname = customer.Surname;

            }
            else
            {
                //add
                int? lastCustomerId = m_Context.Customer.Max(p => p.Id);
                customer.Id = lastCustomerId == null ? 1 : lastCustomerId + 1;

                m_Context.Customer.Add(customer);
            }    

            return customer;
        }

    }

  
}
