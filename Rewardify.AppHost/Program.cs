var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Rewardify_ApiService>("API-Service");

builder.AddProject<Projects.Rewardify_Web>("Rewardify-Web")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.AddProject<Projects.Rewardify_API>("Rewardify-API");

builder.Build().Run();