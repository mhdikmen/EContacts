namespace Contact.API.Models
{
    public class Contact
    {
        public Contact()
        {
            Contacts = [];
        }
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
