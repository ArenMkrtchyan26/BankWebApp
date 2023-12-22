namespace BankWebApp.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string CardId { get; set; }
        public decimal Balance { get; set; }
    }
}
