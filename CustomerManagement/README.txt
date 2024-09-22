This is web api project. Web api was developed as multilayer structure

3 tier architecture is used, 


Manager folder 
is for business layer, which we put business logic and calls the data layer

Data folder 
is for data layer, which we get data from data source. In this example we dont have database, we hardcoded datas in 
CustomerRepository.cs. We could have taken datas from database either accesing database by SPs  directly or 
ORM (Object–relational mapping) frameworks such as EntityFramework

Model Folder 
is for data models which are used through controller, business, and data layers.


*Web apis : GetCustomerById, GetCustomerList, GetCustomerListByName, SaveCustomer   can be found in CustomerController.cs 


*Dependency injections are registered at program.cs 

builder.Services.AddScoped<ICustomerManager, CustomerManager>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<DatabaseContext>();







