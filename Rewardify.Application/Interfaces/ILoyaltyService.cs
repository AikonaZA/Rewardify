using Rewardify.Core.Entities;

namespace Rewardify.Application.Interfaces;

public interface ILoyaltyService
{
    Task EarnPointsAsync(int customerId, decimal purchaseAmount);

    Task RedeemPointsAsync(int customerId, int points);

    Task<Customer?> GetCustomerPointsBalanceAsync(int customerId);
}