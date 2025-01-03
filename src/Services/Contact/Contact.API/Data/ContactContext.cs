namespace Contact.API.Data
{
    public class ContactContext : DbContext
    {
        public DbSet<Models.Contact> Contacts { get; set; } = default!;
        public DbSet<Models.ContactDetail> ContactDetails { get; set; } = default!;

        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                InitialDataGenerator generator = new(1000);
                modelBuilder.Entity<Models.Contact>().HasData(generator.GetContacts());
                modelBuilder.Entity<Models.ContactDetail>().HasData(generator.GetContactDetails());
            }
        }
    }
}
