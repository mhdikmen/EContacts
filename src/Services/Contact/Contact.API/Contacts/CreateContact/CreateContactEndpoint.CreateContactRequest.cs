namespace Contact.API.Contacts.CreateContact
{
    public record CreateContactRequest
    {
        public const string Route = "/contacts";
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
    }
}
