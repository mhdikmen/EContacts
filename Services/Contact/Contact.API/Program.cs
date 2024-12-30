using Contact.API.Data;
using Microsoft.EntityFrameworkCore;

InitialDataGenerator initialDataGenerator = new();
var s = initialDataGenerator.GenerateContacts();
var builder = WebApplication.CreateBuilder(args);

string dbConnectionString = builder.Configuration.GetConnectionString(nameof(ContactContext))!;

builder.Services.AddDbContext<ContactContext>(options =>
{
    options.UseNpgsql(dbConnectionString);
});

builder.AddServiceDefaults();
var app = builder.Build();

app.UseMigration();

app.MapDefaultEndpoints();
app.Run();
