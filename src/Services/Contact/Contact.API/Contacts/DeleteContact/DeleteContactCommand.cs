using Contact.API.Contacts.CreateContact;

namespace Contact.API.Contacts.DeleteContact
{
    public record DeleteContactCommand(Guid Id) : BuildingBlocks.CQRS.ICommand<DeleteContactResult>;

}
