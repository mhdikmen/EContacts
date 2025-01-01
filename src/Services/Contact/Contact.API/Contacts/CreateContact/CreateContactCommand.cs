namespace Contact.API.Contacts.CreateContact
{
    public record CreateContactCommand(string Name, string Surname, string CompanyName) : BuildingBlocks.CQRS.ICommand<CreateContactResult>;

}
