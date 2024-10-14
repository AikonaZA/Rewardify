using Dapper;
using Microsoft.Data.Sqlite;
using Rewardify.Core.Entities;
using Rewardify.Core.Interfaces;

namespace Rewardify.Infrastructure.Data;

public class RewardTransactionRepository : IRewardTransactionRepository
{
    private readonly string _connectionString;

    public RewardTransactionRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddTransactionAsync(RewardTransaction transaction)
    {
        using var connection = new SqliteConnection(_connectionString);
        var sql = @"INSERT INTO RewardTransactions (CustomerId, PurchaseAmount, PointsEarned, PointsRedeemed, TransactionDate)
                            VALUES (@CustomerId, @PurchaseAmount, @PointsEarned, @PointsRedeemed, @TransactionDate)";
        await connection.ExecuteAsync(sql, transaction);
    }

    public async Task<List<RewardTransaction>> GetTransactionsByCustomerIdAsync(int customerId)
    {
        using var connection = new SqliteConnection(_connectionString);
        var sql = "SELECT * FROM RewardTransactions WHERE CustomerId = @CustomerId";
        return (await connection.QueryAsync<RewardTransaction>(sql, new { CustomerId = customerId })).ToList();
    }
}
