using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mongo2Go;
using MongoDB.Driver;

namespace Report.API.Tests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private MongoDbRunner _mongoDbRunner;
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.ConfigureServices(services =>
            {
                _mongoDbRunner = MongoDbRunner.Start();

                var mongoClient = new MongoClient(_mongoDbRunner.ConnectionString);
                services.AddSingleton<IMongoClient>(mongoClient);


                services.AddMassTransit(x =>
                {
                    x.UsingInMemory((context, cfg) =>
                    {
                        cfg.ConfigureEndpoints(context);
                    });
                });

            });

            var host = builder.Build();
            host.Start();
            return host;
        }

        public override ValueTask DisposeAsync()
        {
            _mongoDbRunner.Dispose();
            return base.DisposeAsync();
        }
    }
}
