namespace Contact.API.Contacts.DeleteContactDetail;

internal class DeleteContactDetailCommandHandler(ContactContext _contactContext)
    : BuildingBlocks.CQRS.ICommandHandler<DeleteContactDetailCommand, DeleteContactDetailResult>
{
    public async Task<DeleteContactDetailResult> Handle(DeleteContactDetailCommand request, CancellationToken cancellationToken)
    {
        Models.ContactDetail? contactDetail = await _contactContext.ContactDetails
            .Include(x => x.Contact)
            .FirstOrDefaultAsync(x => x.Id == request.ContactDetailId && x.ContactId == request.ContactId, cancellationToken);
        if (contactDetail == null)
            return new DeleteContactDetailResult(false);

        contactDetail.Contact.ModifiedDate = DateTime.UtcNow;
        _contactContext.ContactDetails.Remove(contactDetail);
        await _contactContext.SaveChangesAsync(cancellationToken);
        return new DeleteContactDetailResult(true);
    }
}