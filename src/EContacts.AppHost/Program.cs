var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Contact_API>("contact-api");

builder.AddProject<Projects.Report_API>("report-api");

builder.Build().Run();
