using Contact.API;

var builder = WebApplication.CreateBuilder(args);
builder.AddCommonApiServices();

if(builder.Environment.IsDevelopment() || builder.Environment.IsProduction())
{
    builder.AddDatabaseApiService();
}

var app = builder.Build();

app.UseCommonApiServices();

await app.RunAsync();

namespace Contact.API
{
    public partial class Program
    {
    }
}
