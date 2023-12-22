using BankWebApp.Services.Interfaces;
using System.Data.SqlClient;
using System.Data;
using BankWebApp.Models;

namespace BankWebApp.Services.Implimentations
{
    public class CardService : ICardService
    {
        private readonly IConfiguration _configuration;
        public CardService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void CreateCardAndAccount(int customerId, decimal balance)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                string query = "EXECUTE CreateAccountAndCard @customer_id, @initial_balance";

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.Add("@customer_id", SqlDbType.Int).Value = customerId;
                    command.Parameters.Add("@initial_balance", SqlDbType.Decimal).Value = balance;
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<CardModel> GetCustomerCards(int customerId)
        {
            List<CardModel> customerCards = new List<CardModel>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                string query = "SELECT TOP (1000) c.[id], c.[card_number], c.[expiration_date], c.[is_blocked], c.[account_id], c.[cvv] " +
                           "FROM [BankDB].[dbo].[Card] c " +
                           "JOIN [BankDB].[dbo].[Account] a ON c.[account_id] = a.[id] " +
                           "WHERE a.[customer_id] = @customerId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@customerId", customerId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CardModel card = new CardModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                CardNumber = reader.GetString(reader.GetOrdinal("card_number")),
                                ExpirationDate = reader.GetDateTime(reader.GetOrdinal("expiration_date")),
                                IsBlocked = reader.GetBoolean(reader.GetOrdinal("is_blocked")),
                                AccountId = reader.GetInt32(reader.GetOrdinal("account_id")),
                                CVV = reader.GetString(reader.GetOrdinal("cvv"))
                            };

                            customerCards.Add(card);
                        }
                    }
                }
            }

            return customerCards;
        
    }
    }
}
