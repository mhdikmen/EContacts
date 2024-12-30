using BuildingBlocks.Enums;

namespace Contact.API.Models
{
    public class ContactDetail
    {
        public Guid Id { get; set; }
        public ContactDetailType Type { get; set; }
        public string Content { get; set; } = default!;
    }
}
