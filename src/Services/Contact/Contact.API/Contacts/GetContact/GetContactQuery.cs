namespace Contact.API.Contacts.GetContact;

public record GetContactQuery(Guid Id) : BuildingBlocks.CQRS.IQuery<GetContactQueryResult>;
