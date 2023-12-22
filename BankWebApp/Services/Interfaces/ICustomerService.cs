using BankWebApp.Models;

namespace BankWebApp.Services.Interfaces
{
    public interface ICustomerService
    {
        List<CustomerModel> GetAllCustomers();
        void InsertCustomer(CustomerModel customer);
        List<CustomerModel> GetCustomersByName(string firstName, string lastName);
        //CustomerModel GetCustomerByName(string name);
        CustomerModel GetCustomerById(int id);
        void InsertRandomCustomers();
    }
}
