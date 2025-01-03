namespace Contact.API.Models
{
    public class Contact
    {
        public Contact()
        {
            ContactDetails = [];
        }
        public Contact(string name, string surname, string companyName)
        {
            Name = name;
            Surname = surname;
            CompanyName = companyName;
            CreatedDate = DateTime.UtcNow;
            ContactDetails = [];
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public virtual ICollection<ContactDetail> ContactDetails { get; set; }
        public void AddContactDetail(ContactDetail contactDetail)
        {
            ContactDetails.Add(contactDetail);
        }
        public void AddContactDetails(IList<ContactDetail> contactDetails)
        {
            foreach (var contactDetail in contactDetails)
            {
                AddContactDetail(contactDetail);
            }
        }
    }
}
