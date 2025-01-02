namespace Contact.API.Models
{
    public class Contact
    {
        public Contact()
        {
            ContactDetails = [];
        }
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public virtual ICollection<ContactDetail> ContactDetails { get; set; }

        public void AddContactDetail(ContactDetail contactDetail)
        {
            ContactDetails.Add(contactDetail);
        }   
    }
}
