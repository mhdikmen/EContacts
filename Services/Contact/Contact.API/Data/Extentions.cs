using Microsoft.EntityFrameworkCore;

namespace Contact.API.Data
{
    public static class Extentions
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<ContactContext>();
            dbContext.Database.MigrateAsync();
            return app;
        }
    }
}
