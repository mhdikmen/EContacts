namespace Contact.API.Contacts.GetContact
{
    public record GetContactRequest
    {
        public const string Route = "/contacts/{Id}";
        public static string BuildRoute(Guid Id) => $"/contacts/{Id}";
        public Guid Id { get; set; }
    }
}
