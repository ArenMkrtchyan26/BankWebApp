namespace BankWebApp.Models
{
    public class CardModel
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsBlocked { get; set; }
        public int AccountId { get; set; }
        public string CVV { get; set; }
    }
}
