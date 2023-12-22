using BankWebApp.Models;
using BankWebApp.Services.Interfaces;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;

namespace BankWebApp.Services.Implimentations
{
    public class CustomerService : ICustomerService
    {
        private readonly IConfiguration _configuration;
        public CustomerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<CustomerModel> GetAllCustomers()
        {
            List<CustomerModel> result = new List<CustomerModel>();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Select * from Customer", connection);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CustomerModel model = new CustomerModel();
                model.Id = int.Parse(dt.Rows[i]["id"].ToString());
                model.BranchId = int.Parse(dt.Rows[i]["branch_id"].ToString());
                model.FirstName = dt.Rows[i]["first_name"].ToString();
                model.LastName = dt.Rows[i]["last_name"].ToString();
                model.DateOfBirth = (DateTime.Parse(dt.Rows[i]["date_of_birth"].ToString()))
                                   .ToString("yyyy/MM/dd");
                model.Gender = dt.Rows[i]["gender"].ToString();
                result.Add(model);
            }
            return result;
        }

        public CustomerModel GetCustomerById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                string query = "SELECT * FROM Customer WHERE  id= @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CustomerModel
                            {
                                Id = (int)reader["id"],
                                BranchId = (int)reader["branch_id"],
                                FirstName = reader["first_name"].ToString(),
                                LastName = reader["last_name"].ToString(),
                                DateOfBirth = reader["date_of_birth"].ToString(),
                                Gender = reader["gender"].ToString()
                            };
                        }
                    }
                }

                return null;
            }
        }

        public CustomerModel GetCustomerByName(string name)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                string query = "SELECT * FROM Customer WHERE  first_name= @first_name";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@first_name", name);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CustomerModel
                            {
                                Id = (int)reader["id"],
                                BranchId = (int)reader["branch_id"],
                                FirstName = reader["first_name"].ToString(),
                                LastName = reader["last_name"].ToString(),
                                DateOfBirth = reader["date_of_birth"].ToString(),
                                Gender = reader["gender"].ToString()
                            };
                        }
                    }
                }

                return null;
            }
        }
        public List<CustomerModel> GetCustomersByName(string firstName, string lastName)
        {
            List<CustomerModel> customers = new List<CustomerModel>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                string query = "SELECT * FROM Customer WHERE first_name = @first_name AND last_name = @last_name";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@first_name", firstName);
                    command.Parameters.AddWithValue("@last_name", lastName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CustomerModel customer = new CustomerModel
                            {
                                Id = (int)reader["id"],
                                BranchId = (int)reader["branch_id"],
                                FirstName = reader["first_name"].ToString(),
                                LastName = reader["last_name"].ToString(),
                                DateOfBirth = reader["date_of_birth"] != DBNull.Value ? reader["date_of_birth"].ToString() : null,
                                Gender = reader["gender"].ToString()
                            };

                            customers.Add(customer);
                        }
                    }
                }
            }

            return customers;
        }

        public void InsertCustomer(CustomerModel customer)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                string query = "INSERT INTO Customer (branch_id, first_name, last_name, date_of_birth, gender) " +
                               "VALUES (@branch_id, @first_name, @last_name, @date_of_birth, @gender)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@branch_id", customer.BranchId);
                    command.Parameters.AddWithValue("@first_name", customer.FirstName);
                    command.Parameters.AddWithValue("@last_name", customer.LastName);
                    command.Parameters.AddWithValue("@date_of_birth", customer.DateOfBirth);
                    command.Parameters.AddWithValue("@gender", customer.Gender);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertRandomCustomers()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                string query = "EXECUTE InsertRandomCustomers";

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
