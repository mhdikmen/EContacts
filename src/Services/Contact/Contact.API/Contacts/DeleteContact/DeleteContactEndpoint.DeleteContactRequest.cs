namespace Contact.API.Contacts.DeleteContact
{
    public record DeleteContactRequest
    {
        public const string Route = "/contacts/{Id}";
        public static string BuildRoute(Guid Id) => $"/contacts/{Id}";
        public Guid Id { get; set; }
    }
}
