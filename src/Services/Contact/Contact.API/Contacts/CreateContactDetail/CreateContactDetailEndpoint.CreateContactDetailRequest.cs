using BuildingBlocks.Enums;

namespace Contact.API.Contacts.CreateContactDetail
{
    public class CreateContactDetailRequest
    {
        public const string Route = "/contacts/{ContactId}";
        public static string BuildRoute(Guid contactId) => $"/contacts/{contactId}";
        public Guid ContactId { get; set; }
        public ContactDetailType Type { get; set; }
        public string Content { get; set; } = default!;
    }
}
