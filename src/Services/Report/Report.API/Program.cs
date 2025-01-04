using Report.API;

var builder = WebApplication.CreateBuilder(args);

builder.AddCommonApiServices();
var app = builder.Build();

app.UseCommonApiServices();

await app.RunAsync();

namespace Report.API
{
    public partial class Program
    {
    }
}
