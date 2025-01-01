using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

Assembly assembly = typeof(Program).Assembly;

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

logger.Information("Starting web host");

string dbConnectionString = builder.Configuration.GetConnectionString(nameof(ContactContext))!;

builder.Services.AddDbContext<ContactContext>(options =>
{
    options.UseNpgsql(dbConnectionString);
});

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddFastEndpoints()
                .SwaggerDocument(o =>
                {
                    o.ShortSchemaNames = true;
                });


builder.AddServiceDefaults();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

//app.UseMiddleware<SerilogRequestResponseLoggingMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.UseFastEndpoints(c =>
{
    c.Errors.ResponseBuilder = (failures, ctx, statusCode) =>
    {
        return new ValidationErrorResponse(failures.Select(x => x.ErrorMessage).ToList());
    };
}).UseSwaggerGen();

app.UseMigration();

app.MapDefaultEndpoints();

await app.RunAsync();
