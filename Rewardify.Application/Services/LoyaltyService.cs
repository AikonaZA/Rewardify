using Rewardify.Application.Interfaces;
using Rewardify.Core.Entities;
using Rewardify.Core.Interfaces;

namespace Rewardify.Application.Services;

public class LoyaltyService(ICustomerRepository customerRepository, IRewardTransactionRepository rewardTransactionRepository) : ILoyaltyService
{
    public async Task EarnPointsAsync(int customerId, decimal purchaseAmount)
    {
        var customer = await customerRepository.GetCustomerByIdAsync(customerId) ?? throw new InvalidOperationException("Customer not found.");

        // Assume 1 point per $10 spent
        int pointsEarned = (int)(purchaseAmount / 10);
        customer.PointsBalance += pointsEarned;

        // Add a transaction record
        var transaction = new RewardTransaction
        {
            CustomerId = customerId,
            PurchaseAmount = purchaseAmount,
            PointsEarned = pointsEarned,
            TransactionDate = DateTime.UtcNow
        };

        await rewardTransactionRepository.AddTransactionAsync(transaction);
        await customerRepository.UpdateCustomerAsync(customer);
    }

    public async Task RedeemPointsAsync(int customerId, int points)
    {
        var customer = await customerRepository.GetCustomerByIdAsync(customerId) ?? throw new InvalidOperationException("Customer not found.");
        if (customer.PointsBalance < points) throw new InvalidOperationException("Insufficient points.");

        // Deduct points from the customer's balance
        customer.PointsBalance -= points;

        // Add a transaction to record redemption
        var transaction = new RewardTransaction
        {
            CustomerId = customerId,
            PurchaseAmount = 0, // No purchase in redemption
            PointsEarned = 0,
            PointsRedeemed = points,
            TransactionDate = DateTime.UtcNow
        };

        await rewardTransactionRepository.AddTransactionAsync(transaction);
        await customerRepository.UpdateCustomerAsync(customer);
    }

    public async Task<Customer?> GetCustomerPointsBalanceAsync(int customerId) => await customerRepository.GetCustomerByIdAsync(customerId);
}