using BuildingBlocks.Middlewares;
using EContacts.ServiceDefaults;

namespace Contact.API
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

            return builder;
        }

        public static WebApplicationBuilder AddDatabaseApiService(this WebApplicationBuilder builder)
        {
            string dbConnectionString = builder.Configuration.GetConnectionString(nameof(ContactContext))!;

            builder.Services.AddDbContext<ContactContext>(options =>
            {
                options.UseNpgsql(dbConnectionString);
            });

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


            app.UseMigration();

            app.MapDefaultEndpoints();

            return app;
        }
    }
}
