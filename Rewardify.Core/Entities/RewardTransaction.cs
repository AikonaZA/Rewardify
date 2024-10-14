namespace Rewardify.Core.Entities;

public class RewardTransaction
{
    public int TransactionId { get; set; }
    public int CustomerId { get; set; }
    public decimal PurchaseAmount { get; set; }
    public int PointsEarned { get; set; }
    public int PointsRedeemed { get; set; } = 0; // Default to no points redeemed
    public DateTime TransactionDate { get; set; }
}