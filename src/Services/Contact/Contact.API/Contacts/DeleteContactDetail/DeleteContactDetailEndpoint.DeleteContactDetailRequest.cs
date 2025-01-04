namespace Contact.API.Contacts.DeleteContactDetail
{
    public record DeleteContactDetailRequest
    {
        public const string Route = "/contacts/{ContactId}/contactdetail/{ContactDetailId}";
        public static string BuildRoute(Guid contactId, Guid contactDetailId) => $"/contacts/{contactId}/contactdetail/{contactDetailId}";
        public Guid ContactId { get; set; }
        public Guid ContactDetailId { get; set; }
    }
}
