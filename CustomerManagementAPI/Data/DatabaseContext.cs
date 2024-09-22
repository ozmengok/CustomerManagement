using CustomerWebApi.Model;

namespace CustomerWebApi.Data
{
    public class DatabaseContext
    {
        private readonly IConfiguration m_Configuration;
        public  List<Customer> Customer { get; set; }


        public DatabaseContext(IConfiguration configuration) 
        {
            m_Configuration = configuration;
            Customer = GetCustomerDatas();
        }


        public List<Customer> GetCustomerDatas() 
        {

            //Use m_Configuration if you want to access database to get datas
            //var connectionString = m_Configuration.GetValue<string>("ConnectionString");

            return new List<Customer>
            {
                new Customer { Id= 1, Firstname = "Jack", Surname = "AAA", Status = "A"},
                new Customer { Id= 2, Firstname = "Yasar", Surname = "BBBB", Status = "A"},
                new Customer { Id= 3, Firstname = "Ozmen", Surname = "CCC", Status = "P"},
                new Customer { Id= 4, Firstname = "XXXXX", Surname = "DDDD", Status = "A"},
                new Customer { Id= 5, Firstname = "YYYY", Surname = "EEEE", Status = "A"}
            };
        }


    }
}
