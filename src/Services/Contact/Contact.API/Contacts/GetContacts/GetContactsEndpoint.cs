using MediatR;

namespace Contact.API.Contacts.GetContacts
{
    public class GetContactsEndpoint(IMediator _mediator) : Endpoint<GetContactsRequest, GetContactsResponse>
    {
        public override void Configure()
        {
            Get(GetContactsRequest.Route);
            AllowAnonymous();

            Description(b => b
                .WithName("GetContacts")
                .Accepts<GetContactsRequest>()
                .Produces<GetContactsResponse>((int)HttpStatusCode.OK, "application/json")
                .Produces((int)HttpStatusCode.NotFound)
                .ProducesProblemFE<ValidationErrorResponse>((int)HttpStatusCode.BadRequest, "application/json")
                .ProducesProblemFE<BuildingBlocks.Responses.ErrorResponse>((int)HttpStatusCode.InternalServerError, "application/json"),
                 clearDefaults: true);

            Summary(s =>
            {
                s.ExampleRequest = new GetContactsRequest { PageIndex = 0, PageSize = 100 };
                s.Summary = "Gets contacts with the details.";
                s.Description = "This endpoint allows you to get contacts.";
                s.Responses[(int)HttpStatusCode.OK] = "Gets the list of contacts.";
                s.Responses[(int)HttpStatusCode.Created] = "Invalid input.";
                s.Responses[(int)HttpStatusCode.InternalServerError] = "Server Error.";
            });
        }
        public override async Task HandleAsync(GetContactsRequest request, CancellationToken ct)
        {
            GetContactsQuery command = request.Adapt<GetContactsQuery>();
            var result = await _mediator.Send(command, ct);
            GetContactsResponse response = result.Adapt<GetContactsResponse>();
            await SendOkAsync(response, ct);
        }
    }
}
