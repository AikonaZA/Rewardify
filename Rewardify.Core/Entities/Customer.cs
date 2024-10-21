namespace Rewardify.Core.Entities;

public class Customer
{
    public int CustomerId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public int PointsBalance { get; set; } = 0; // Default to 0 points

    // To maintain history of transactions for the customer
    public List<RewardTransaction> Transactions { get; set; } = [];
}