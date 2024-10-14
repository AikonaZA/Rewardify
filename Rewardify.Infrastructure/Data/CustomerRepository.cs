using Dapper;
using Rewardify.Core.Entities;
using Rewardify.Core.Interfaces;
using Microsoft.Data.Sqlite;

namespace Rewardify.Infrastructure.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                var sql = "SELECT * FROM Customers WHERE CustomerId = @CustomerId";
                return await connection.QueryFirstOrDefaultAsync<Customer>(sql, new { CustomerId = customerId });
            }
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                var sql = "INSERT INTO Customers (Name, Email, PointsBalance) VALUES (@Name, @Email, @PointsBalance)";
                await connection.ExecuteAsync(sql, customer);
            }
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                var sql = "UPDATE Customers SET Name = @Name, Email = @Email, PointsBalance = @PointsBalance WHERE CustomerId = @CustomerId";
                await connection.ExecuteAsync(sql, customer);
            }
        }
    }
}
