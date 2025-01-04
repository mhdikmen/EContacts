namespace Contact.API.Contacts.GetContact;

internal class GetContactsQueryHandler(ContactContext _contactContext)
    : IQueryHandler<GetContactsQuery, GetContactsQueryResult>
{
    public async Task<GetContactsQueryResult> Handle(GetContactsQuery request, CancellationToken cancellationToken)
    {
        IOrderedQueryable<Models.Contact> contactsQ = _contactContext.Contacts
            .AsNoTracking()
            .Include(x => x.ContactDetails)
            .OrderBy(x => x.CreatedDate);

        long totalCount = await contactsQ.LongCountAsync(cancellationToken);
        long pageCount = (long)Math.Floor((double)totalCount / request.PageSize);

        List<Models.Contact> contacts = await contactsQ
            .Skip(request.PageIndex * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new GetContactsQueryResult(new(request.PageIndex, request.PageSize, totalCount, pageCount, contacts));
    }
}