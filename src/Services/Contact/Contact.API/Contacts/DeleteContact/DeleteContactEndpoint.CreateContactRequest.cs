namespace Contact.API.Contacts.CreateContact
{
    public record DeleteContactRequest
    {
        public const string Route = "/contacts/{Id}";
        public static string BuidRoute(Guid Id) => $"/contacts/{Id}";
        public Guid Id { get; set; }
    }
}
