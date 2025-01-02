using BuildingBlocks.Enums;
using Contact.API.Contacts.CreateContact;
using MediatR;

namespace Contact.API.Contacts.CreateContactDetail
{
    public class CreateContactDetailEndpoint(IMediator _mediator) : Endpoint<CreateContactDetailRequest, CreateContactDetailResponse>
    {
        public override void Configure()
        {
            Patch(CreateContactDetailRequest.Route);
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Create a contact detail.";
                s.Description = "This endpoint allows you to create a contact detail for a contact.";
                s.ExampleRequest = new CreateContactDetailRequest
                {
                    ContactId = Guid.NewGuid(),
                    Type = ContactDetailType.EmailAddress,
                    Content = "example@example.com"
                };
                s.Responses[200] = "The contact detail was created successfully.";
                s.Responses[404] = "The contact was not found.";
                s.Responses[400] = "Invalid input, such as an unsupported enum value.";
            });
        }
        public override async Task HandleAsync(CreateContactDetailRequest request, CancellationToken ct)
        {
            CreateContactDetailCommand command = request.Adapt<CreateContactDetailCommand>();
            var result = await _mediator.Send(command, ct);
            if (result.IsSuccess)
                await SendOkAsync(new CreateContactDetailResponse(result.Id.GetValueOrDefault()), ct);
            else
                await SendNotFoundAsync(ct);
        }
    }
}
