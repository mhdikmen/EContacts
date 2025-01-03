namespace Contact.API.Data
{
    public static class Extentions
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<ContactContext>();

            if (dbContext.Database.IsRelational())
                dbContext.Database.Migrate();

            dbContext.Database.EnsureCreated();
            return app;
        }
    }
}
