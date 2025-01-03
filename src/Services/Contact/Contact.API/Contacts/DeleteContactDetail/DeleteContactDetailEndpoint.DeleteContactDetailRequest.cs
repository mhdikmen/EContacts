namespace Contact.API.Contacts.DeleteContactDetail
{
    public record DeleteContactDetailRequest
    {
        public const string Route = "/contacts/{ContactId}/contactdetail/{ContactDetailId}";
        public static string BuidRoute(Guid ContactId, Guid ContactDetailId) => $"/contacts/{ContactId}/contactdetail/{ContactDetailId}";
        public Guid ContactId { get; set; }
        public Guid ContactDetailId { get; set; }
    }
}
