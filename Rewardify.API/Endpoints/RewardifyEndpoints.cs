using Rewardify.Application.Interfaces;

namespace Rewardify.API.Endpoints;

public static class RewardifyEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/api/rewardify/earn", async (int customerId, decimal amount, ILoyaltyService loyaltyService) =>
        {
            await loyaltyService.EarnPointsAsync(customerId, amount);
            return Results.Ok("Points earned successfully.");
        });

        app.MapPost("/api/rewardify/redeem", async (int customerId, int points, ILoyaltyService loyaltyService) =>
        {
            try
            {
                await loyaltyService.RedeemPointsAsync(customerId, points);
                return Results.Ok("Points redeemed successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        app.MapGet("/api/rewardify/balance/{customerId}", async (int customerId, ILoyaltyService loyaltyService) =>
        {
            var customer = await loyaltyService.GetCustomerPointsBalanceAsync(customerId);
            return customer == null ? Results.NotFound("Customer not found.") : Results.Ok(customer.PointsBalance);
        });
    }
}