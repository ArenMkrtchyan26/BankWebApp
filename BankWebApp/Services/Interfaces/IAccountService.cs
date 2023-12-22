using BankWebApp.Models;

namespace BankWebApp.Services.Interfaces
{
    public interface IAccountService
    {
        void OpenAccount(int customerId, decimal balance);
        List<AccountModel> GetAccountsCustomer(int customerId);
    }
}
