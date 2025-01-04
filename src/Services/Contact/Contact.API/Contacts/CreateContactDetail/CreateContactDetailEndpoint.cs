using BuildingBlocks.Enums;
using Contact.API.Contacts.CreateContact;
using MediatR;
using System.Linq;

namespace Contact.API.Contacts.CreateContactDetail
{
    public class CreateContactDetailEndpoint(IMediator _mediator) : Endpoint<CreateContactDetailRequest, CreateContactDetailResponse>
    {
        public override void Configure()
        {
            Patch(CreateContactDetailRequest.Route);
            AllowAnonymous();

            Description(b => b
                .WithName("CreateContactDetail")
                .Accepts<CreateContactDetailRequest>("application/json")
                .Produces<CreateContactDetailResponse>((int)HttpStatusCode.OK, "application/json")
                .Produces((int)HttpStatusCode.NotFound)
                .ProducesProblemFE<ValidationErrorResponse>((int)HttpStatusCode.BadRequest, "application/json")
                .ProducesProblemFE<BuildingBlocks.Responses.ErrorResponse>((int)HttpStatusCode.InternalServerError, "application/json"),
                 clearDefaults: true);

            Summary(s =>
            {
                s.ExampleRequest = new CreateContactDetailRequest
                {
                    ContactId = Guid.NewGuid(),
                    Type = ContactDetailType.EmailAddress,
                    Content = "example@example.com"
                };

                s.Summary = "Create a contact detail.";
                s.Description = string.Concat("This endpoint allows you to create a contact detail for a contact.", "Valid contact detail types are ", string.Join(" , ", Enum.GetValues<ContactDetailType>().Select(e => string.Concat(((int)e).ToString(), " - ", e))));
                s.Responses[(int)HttpStatusCode.OK] = "The contact detail was created successfully.";
                s.Responses[(int)HttpStatusCode.NotFound] = "The contact was not found.";
                s.Responses[(int)HttpStatusCode.Created] = "Invalid input, such as an unsupported enum value.";
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
