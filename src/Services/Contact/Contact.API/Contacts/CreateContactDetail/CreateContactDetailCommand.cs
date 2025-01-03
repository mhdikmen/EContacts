using BuildingBlocks.Enums;

namespace Contact.API.Contacts.CreateContactDetail
{
    public record CreateContactDetailCommand(Guid ContactId, ContactDetailType Type, string Content) : BuildingBlocks.CQRS.ICommand<CreateContactDetailResult>;

}
