using BuildingBlocks.Middlewares;
using EContacts.ServiceDefaults;
using MongoDB.Driver;
using Report.API.Data;
using Report.API.Services.Contact;
using Report.API.Services.Report;

namespace Report.API
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder AddCommonApiServices(this WebApplicationBuilder builder)
        {
            Assembly assembly = typeof(Program).Assembly;

            var logger = Log.Logger = new LoggerConfiguration()
              .Enrich.FromLogContext()
              .WriteTo.Console()
              .CreateLogger();

            logger.Information("Starting web host");

            builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

            builder.Services.AddHttpClient("Contact", u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ContactAPI"]!));
            //Async Communication Services
            builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());
            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddFastEndpoints()
                            .SwaggerDocument();


            builder.AddServiceDefaults();

            builder.Services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetValue<string>("MongoDbSettings:ConnectionString");
                return new MongoClient(connectionString);
            });

            builder.Services.AddScoped<IReportRepository, ReportRepository>();
            builder.Services.AddScoped<IContactService, ContactService>();
            builder.Services.AddScoped<IReportService, ReportService>();
            return builder;
        }

        public static WebApplication UseCommonApiServices(this WebApplication app)
        {
            app.UseMiddleware<SerilogRequestResponseLoggingMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseFastEndpoints(c =>
            {
                c.Errors.ResponseBuilder = (failures, ctx, statusCode) =>
                {
                    return new ValidationErrorResponse(failures.Select(x => x.ErrorMessage).ToList());
                };
            }).UseSwaggerGen();

            app.MapDefaultEndpoints();

            return app;
        }
    }
}
