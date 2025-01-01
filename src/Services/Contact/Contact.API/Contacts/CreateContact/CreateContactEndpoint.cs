using MediatR;

namespace Contact.API.Contacts.CreateContact
{
    public class CreateContactEndpoint(IMediator _mediator) : Endpoint<CreateContactRequest, CreateContactResponse>
    {
        public override void Configure()
        {
            Post(CreateContactRequest.Route);
            AllowAnonymous();
            Summary(s =>
            {
                s.ExampleRequest = new CreateContactRequest { Name = "John", Surname = "Doe", CompanyName = "ABC Systems" };
            });
        }
        public override async Task HandleAsync(CreateContactRequest request, CancellationToken ct)
        {
            CreateContactCommand command = request.Adapt<CreateContactCommand>();
            var result = await _mediator.Send(command, ct);
            var contactUrl = $"{CreateContactRequest.Route}/{result.Id}";
            var response = new CreateContactResponse(result.Id, contactUrl);
            await SendCreatedAtAsync(contactUrl, result.Id, response, true, ct);
        }
    }
}
