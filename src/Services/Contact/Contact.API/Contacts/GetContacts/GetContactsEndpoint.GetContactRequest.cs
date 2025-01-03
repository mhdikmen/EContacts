namespace Contact.API.Contacts.GetContact
{
    public record GetContactsRequest
    {
        public const string Route = "/contacts";
        public static string BuidRoute(Guid Id) => $"/contacts/{Id}";
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}
