var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Rewardify_ApiService>("apiservice");

builder.AddProject<Projects.Rewardify_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.AddProject<Projects.Rewardify_API>("rewardify-api");

builder.Build().Run();
