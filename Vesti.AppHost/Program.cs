var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Vesti_ApiService>("apiservice");

builder.AddProject<Projects.Vesti_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
