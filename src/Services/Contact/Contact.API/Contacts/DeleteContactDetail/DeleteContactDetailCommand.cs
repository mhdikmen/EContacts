namespace Contact.API.Contacts.DeleteContactDetail;

public record DeleteContactDetailCommand(Guid ContactId, Guid ContactDetailId) : BuildingBlocks.CQRS.ICommand<DeleteContactDetailResult>;
