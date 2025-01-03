namespace Contact.API.Contacts.CreateContact
{
    public class CreateContactCommandHandler(ContactContext _contactContext)
        : BuildingBlocks.CQRS.ICommandHandler<CreateContactCommand, CreateContactResult>
    {
        public async Task<CreateContactResult> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            Models.Contact contact = request.Adapt<Models.Contact>();
            contact.CreatedDate = DateTime.UtcNow;
            await _contactContext.Contacts.AddAsync(contact);
            await _contactContext.SaveChangesAsync(cancellationToken);
            CreateContactResult result = contact.Adapt<CreateContactResult>();
            return result;
        }
    }
}