
namespace Contact.API.Contacts.CreateContact
{
    public class CreateContactCommandHandler(ContactContext contactContext)
        : BuildingBlocks.CQRS.ICommandHandler<CreateContactCommand, CreateContactResult>
    {
        public async Task<CreateContactResult> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            Models.Contact contact = request.Adapt<Models.Contact>();
            await contactContext.Contacts.AddAsync(contact);
            await contactContext.SaveChangesAsync();
            return new CreateContactResult(contact.Id);
        }
    }
}