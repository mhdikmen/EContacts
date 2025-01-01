namespace Contact.API.Contacts.CreateContact
{
    public record DeleteContactResponse : BaseResponse
    {
        public DeleteContactResponse(System.Net.HttpStatusCode httpStatusCode, string message) : base(httpStatusCode, message)
        {

        }
    }
}
