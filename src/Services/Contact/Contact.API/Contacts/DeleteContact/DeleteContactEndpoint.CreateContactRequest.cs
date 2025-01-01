namespace Contact.API.Contacts.CreateContact
{
    public class DeleteContactRequest
    {
        public const string Route = "/contacts/{Id}";
        public Guid Id { get; set; }
    }
}
