using BankWebApp.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using BankWebApp.Models;

namespace BankWebApp.Services.Implimentations
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        public AccountService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<AccountModel> GetAccountsCustomer(int customerId)
        {
            List<AccountModel> result = new List<AccountModel>();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            string query = $"SELECT * FROM Account WHERE  customer_id= {customerId}";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AccountModel model = new AccountModel();
                model.Id = int.Parse(dt.Rows[i]["id"].ToString());
                model.Number = dt.Rows[i]["account_number"].ToString();
                model.CardId = dt.Rows[i]["card_id"].ToString()==""?"No Card"
                               : dt.Rows[i]["card_id"].ToString();
                model.Balance =decimal.Parse(dt.Rows[i]["balance"].ToString());
                
                result.Add(model);
            }
            return result;
        }

        public void OpenAccount(int customerId, decimal balance)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                string query = "EXECUTE OpenAccount @customer_id, @initial_balance";

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.Add("@customer_id", SqlDbType.Int).Value = customerId;
                    command.Parameters.Add("@initial_balance", SqlDbType.Decimal).Value = balance;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
