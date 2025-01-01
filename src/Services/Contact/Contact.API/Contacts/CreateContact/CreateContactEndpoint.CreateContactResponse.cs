using BuildingBlocks.Consts;

namespace Contact.API.Contacts.CreateContact
{
    public record CreateContactResponse : BaseResponse
    {
        public CreateContactResponse(Guid id, string location) : base(System.Net.HttpStatusCode.Created, Messages.CreatedMessage)
        {
            Id = id;
            Location = location;
        }
        public Guid Id { get; set; }
        public string Location { get; set; }
    }
}
