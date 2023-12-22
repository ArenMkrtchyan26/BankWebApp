using BankWebApp.Models;

namespace BankWebApp.Services.Interfaces
{
    public interface ICardService
    {
        void CreateCardAndAccount(int customerId, decimal balance);
        List<CardModel> GetCustomerCards(int customerId);
    }
}
