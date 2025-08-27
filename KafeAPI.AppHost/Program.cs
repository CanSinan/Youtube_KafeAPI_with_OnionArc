var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.KafeAPI_API>("kafeapi-api");

builder.Build().Run();
