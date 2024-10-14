using Rewardify.Core.Entities;

namespace Rewardify.Core.Interfaces;

public interface IRewardTransactionRepository
{
    Task AddTransactionAsync(RewardTransaction transaction);
    Task<List<RewardTransaction>> GetTransactionsByCustomerIdAsync(int customerId);
}
