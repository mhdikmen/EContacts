using BuildingBlocks.Enums;

namespace Contact.API.Models
{
    public class ContactDetail
    {
        public Guid Id { get; set; }
        public Guid ContactId { get; set; }
        public ContactDetailType Type { get; set; }
        public string Content { get; set; } = default!;
        public virtual Contact Contact { get; set; } = default!;
    }
}
