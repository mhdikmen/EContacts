using BuildingBlocks.Pagination;

namespace Contact.API.Contacts.GetContact;

public record GetContactsQuery : PaginationRequest, BuildingBlocks.CQRS.IQuery<GetContactsQueryResult>;