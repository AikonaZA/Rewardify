using Microsoft.EntityFrameworkCore;
using Rewardify.API.Endpoints;
using Rewardify.Application.Interfaces;
using Rewardify.Application.Services;
using Rewardify.Core.Interfaces;
using Rewardify.Infrastructure.Data;
using Rewardify.Infrastructure.Repositories;
using Rewardify.ServiceDefaults;
using static Rewardify.Infrastructure.Data.RewardifyDbContext;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the connection string
string connectionString = builder.Configuration.GetConnectionString("RewardifyDatabase")!;

// Add DbContext for SQLite with code-first approach
builder.Services.AddDbContext<RewardifyDbContext>(options =>
    options.UseSqlite(connectionString));

// Register LoyaltyService and required repositories
builder.Services.AddScoped<ILoyaltyService, LoyaltyService>();
builder.Services.AddScoped<ICustomerRepository>(provider => new CustomerRepository(connectionString));
builder.Services.AddScoped<IRewardTransactionRepository>(provider => new RewardTransactionRepository(connectionString));

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Apply database creation
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RewardifyDbContext>();
    RewardifyDbInitializer.Initialize(dbContext);
}
// Map Rewardify Endpoints
RewardifyEndpoints.Map(app);

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}