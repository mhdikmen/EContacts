namespace Contact.API.Contacts.CreateContactDetail
{
    public class CreateContactDetailCommandHandler(ContactContext _contactContext)
        : BuildingBlocks.CQRS.ICommandHandler<CreateContactDetailCommand, CreateContactDetailResult>
    {
        public async Task<CreateContactDetailResult> Handle(CreateContactDetailCommand request, CancellationToken cancellationToken)
        {
            Models.Contact? contact = await _contactContext.Contacts.FindAsync(request.ContactId, cancellationToken);

            if (contact is null)
                return new CreateContactDetailResult(false);

            Models.ContactDetail contactDetail = new(request.Type, request.Content);
            contact.AddContactDetail(contactDetail);
            _contactContext.Contacts.Update(contact);
            await _contactContext.SaveChangesAsync(cancellationToken);
            return new CreateContactDetailResult(true, contactDetail.Id);
        }
    }
}