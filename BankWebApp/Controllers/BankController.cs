using BankWebApp.Models;
using BankWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace BankWebApp.Controllers
{
    [ApiController]
    public class BankController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly ICardService _cardService;
        public BankController(ICardService cardService, 
                              ICustomerService customerService,
                              IAccountService accountService)
        {
            _cardService=cardService;
            _customerService = customerService;
            _accountService = accountService;
        }
    
        [HttpGet]
        [Route("GetAllCustomer")]
        public List<CustomerModel> GetAllCustomer()
        {
            return _customerService.GetAllCustomers();
        }
        [HttpPost]
        [Route("InsertCustomer")]
        public void InsertCustomer(CustomerModel customer)
        {
            _customerService.InsertCustomer(customer);
        }
        [HttpGet]
        [Route("GetCustomerByName")]
        public  List<CustomerModel> GetCustomerByName(string first_name,string last_name)
        {
            return _customerService.GetCustomersByName(first_name, last_name);
        }
        [HttpGet]
        [Route("GetCustomerById")]
        public CustomerModel GetCustomerById(int id)
        {
            return _customerService.GetCustomerById(id);
        }
        [HttpPost]
        [Route("InsertCustomer100")]
        public void CreateCustomers()
        {
            _customerService.InsertRandomCustomers();
        }
        [HttpPost]
        [Route("OpenAccount")]
        public void OpenAccount(int customerId,decimal balance)
        {
            _accountService.OpenAccount(customerId, balance);
        }
        [HttpGet]
        [Route("GetAccountsCustomer")]
        public List<AccountModel> GetAccountsCustomer(int customerId)
        {
            return _accountService.GetAccountsCustomer(customerId);
        }
        [HttpPost]
        [Route("CrateCardAndAccount")]
        public void CrateCardAndAccount(int customerId, decimal balance)
        {
            _cardService.CreateCardAndAccount(customerId, balance);
        }
        [HttpGet]
        [Route("GetCustomerCards")]
        public List<CardModel> GetCustomerCards(int customerId)
        {
            return _cardService.GetCustomerCards(customerId);
        }
    }

}


