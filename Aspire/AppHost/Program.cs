var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CreditAccountApi>("creditaccountapi");

builder.Build().Run();
