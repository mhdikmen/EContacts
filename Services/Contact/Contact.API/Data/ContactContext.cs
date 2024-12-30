using Microsoft.EntityFrameworkCore;

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
            if (!Contacts.Any())
            {
                InitialDataGenerator initialDataGenerator = new();
                modelBuilder.Entity<Models.Contact>().HasData(initialDataGenerator.GenerateContacts());
            }
        }
    }
}
