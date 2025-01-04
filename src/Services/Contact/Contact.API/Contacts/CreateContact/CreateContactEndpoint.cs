using MediatR;

namespace Contact.API.Contacts.CreateContact
{
    public class CreateContactEndpoint(IMediator _mediator) : Endpoint<CreateContactRequest, CreateContactResponse>
    {
        public override void Configure()
        {
            Post("/contacts");
            AllowAnonymous();

            Description(b => b
                .WithName("CreateContact")
                .Accepts<CreateContactRequest>("application/json")
                .Produces<CreateContactResponse>((int)HttpStatusCode.Created, "application/json")
                .ProducesProblemFE<ValidationErrorResponse>((int)HttpStatusCode.BadRequest, "application/json")
                .ProducesProblemFE<BuildingBlocks.Responses.ErrorResponse>((int)HttpStatusCode.InternalServerError, "application/json"),
                 clearDefaults: true);

            Summary(s =>
            {
                s.ExampleRequest = new CreateContactRequest { Name = "John", Surname = "Doe", CompanyName = "ABC Systems" };
                s.Summary = "Create a contact.";
                s.Description = "This endpoint allows you to create a contact.";
                s.Responses[(int)HttpStatusCode.Created] = "The contact was created successfully.";
                s.Responses[(int)HttpStatusCode.Created] = "Invalid input.";
                s.Responses[(int)HttpStatusCode.InternalServerError] = "Server Error.";
            });

        }
        public override async Task HandleAsync(CreateContactRequest request, CancellationToken ct)
        {
            CreateContactCommand command = request.Adapt<CreateContactCommand>();
            var result = await _mediator.Send(command, ct);
            CreateContactResponse response = result.Adapt<CreateContactResponse>();
            await SendCreatedAtAsync("GetContact", new { result.Id }, response, false, ct);
        }
    }
}
