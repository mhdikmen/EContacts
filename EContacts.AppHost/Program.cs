var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Contact_API>("contact-api");

builder.Build().Run();
