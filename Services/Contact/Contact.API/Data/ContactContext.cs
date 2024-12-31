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

            InitialDataGenerator generator = new();
            modelBuilder.Entity<Models.Contact>().HasData(generator.GetContacts());
            modelBuilder.Entity<Models.ContactDetail>().HasData(generator.GetContactDetails());
        }
    }
}
