namespace Contact.API.Contacts.GetContacts
{
    public record GetContactsRequest
    {
        public const string Route = "/contacts";
        public static string BuidRoute(int pageIndex, int pageSize) => $"/contacts?pageIndex={pageIndex}&pageSize={pageSize}";
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}
