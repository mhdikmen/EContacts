using BuildingBlocks.Consts;

namespace Contact.API.Contacts.DeleteContact
{
    internal class DeleteContactCommandHandler(ContactContext contactContext)
        : BuildingBlocks.CQRS.ICommandHandler<DeleteContactCommand, DeleteContactResult>
    {
        public async Task<DeleteContactResult> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            Models.Contact? contact = await contactContext.Contacts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (contact == null)
                return new DeleteContactResult(false, Messages.NotFoundMessage);

            contactContext.Contacts.Remove(contact);
            await contactContext.SaveChangesAsync();
            return new DeleteContactResult(true, Messages.DeletedMessage);
        }
    }
}