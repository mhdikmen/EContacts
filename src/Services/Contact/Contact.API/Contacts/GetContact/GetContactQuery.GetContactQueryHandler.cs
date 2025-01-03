using BuildingBlocks.Dtos.ContactDtos;

namespace Contact.API.Contacts.GetContact;

internal class GetContactQueryHandler(ContactContext _contactContext)
    : IQueryHandler<GetContactQuery, GetContactQueryResult>
{
    public async Task<GetContactQueryResult> Handle(GetContactQuery request, CancellationToken cancellationToken)
    {
        Models.Contact? contact = await _contactContext.Contacts
            .AsNoTracking()
            .Include(x => x.ContactDetails)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (contact == null)
            return new GetContactQueryResult(false);

        ContactDto contactDto = contact.Adapt<ContactDto>();
        return new GetContactQueryResult(true, contactDto);
    }
}