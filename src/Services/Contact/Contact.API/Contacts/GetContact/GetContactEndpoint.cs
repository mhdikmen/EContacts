using MediatR;
using System.Net;

namespace Contact.API.Contacts.GetContact
{
    public class GetContactEndpoint(IMediator _mediator) : Endpoint<GetContactRequest, GetContactResponse>
    {
        public override void Configure()
        {
            Get(GetContactRequest.Route);
            AllowAnonymous();

            Description(b => b
                .WithName("GetContact")
                .Accepts<GetContactRequest>()
                .Produces<GetContactResponse>((int)HttpStatusCode.OK, "application/json")
                .Produces((int)HttpStatusCode.NotFound)
                .ProducesProblemFE<ValidationErrorResponse>((int)HttpStatusCode.BadRequest, "application/json")
                .ProducesProblemFE<BuildingBlocks.Responses.ErrorResponse>((int)HttpStatusCode.InternalServerError, "application/json"),
                 clearDefaults: true);

            Summary(s =>
            {
                s.ExampleRequest = new GetContactRequest { Id = Guid.NewGuid() };
                s.Summary = "Gets a contact with the details.";
                s.Description = "This endpoint allows you to get a contact.";
                s.Responses[(int)HttpStatusCode.OK] = "Gets single a contact information if exists.";
                s.Responses[(int)HttpStatusCode.Created] = "Invalid input.";
                s.Responses[(int)HttpStatusCode.InternalServerError] = "Server Error.";
            });
        }
        public override async Task HandleAsync(GetContactRequest request, CancellationToken ct)
        {
            GetContactQuery command = request.Adapt<GetContactQuery>();
            var result = await _mediator.Send(command, ct);
            if (!result.IsExists)
                await SendNotFoundAsync(ct);
            else
            {
                GetContactResponse response = result.Adapt<GetContactResponse>();
                await SendOkAsync(response, ct);
            }
        }
    }
}
