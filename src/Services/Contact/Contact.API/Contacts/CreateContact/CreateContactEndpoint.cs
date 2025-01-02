using MediatR;

namespace Contact.API.Contacts.CreateContact
{
    public class CreateContactEndpoint(IMediator _mediator) : Endpoint<CreateContactRequest, CreateContactResponse>
    {
        public override void Configure()
        {
            Post(CreateContactRequest.Route);
            AllowAnonymous();

            Description(b => b
                .Accepts<CreateContactRequest>("application/json")
                .Produces<CreateContactResponse>(201, "application/json")
                .ProducesProblemFE<ValidationErrorResponse>(400, "application/json")
                .ProducesProblemFE<BuildingBlocks.Responses.ErrorResponse>(500, "application/json"),
                 clearDefaults: true);

            Summary(s =>
            {
                s.Summary = "Create a contact.";
                s.Description = "This endpoint allows you to create a contact.";
                s.ExampleRequest = new CreateContactRequest { Name = "John", Surname = "Doe", CompanyName = "ABC Systems" };
                s.Responses[201] = "The contact was created successfully.";
                s.Responses[400] = "Invalid input, such as an unsupported enum value.";
                s.Responses[500] = "Server Error";
            });

        }
        public override async Task HandleAsync(CreateContactRequest request, CancellationToken ct)
        {
            CreateContactCommand command = request.Adapt<CreateContactCommand>();
            var result = await _mediator.Send(command, ct);
            var contactUrl = $"{CreateContactRequest.Route}/{result.Id}";
            CreateContactResponse response = result.Adapt<CreateContactResponse>();
            await SendCreatedAtAsync(contactUrl, response.Id, response, true, ct);
        }
    }
}
