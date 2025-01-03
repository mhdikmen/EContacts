using BuildingBlocks.Consts;
using MediatR;

namespace Contact.API.Contacts.DeleteContact
{
    public class DeleteContactEndpoint(IMediator _mediator) : Endpoint<DeleteContactRequest>
    {
        public override void Configure()
        {
            Delete(DeleteContactRequest.Route);
            AllowAnonymous();

            Description(b => b
                .Accepts<DeleteContactRequest>()
                .Produces((int)HttpStatusCode.NoContent)
                .Produces((int)HttpStatusCode.NotFound)
                .ProducesProblemFE<ValidationErrorResponse>((int)HttpStatusCode.BadRequest, "application/json")
                .ProducesProblemFE<BuildingBlocks.Responses.ErrorResponse>((int)HttpStatusCode.InternalServerError, "application/json"),
                 clearDefaults: true);

            Summary(s =>
            {
                s.ExampleRequest = new DeleteContactRequest { Id = Guid.NewGuid() };
                s.Summary = "Delete a contact.";
                s.Description = "This endpoint allows you to delete a contact.";
                s.Responses[(int)HttpStatusCode.NoContent] = "The contact was deleted successfully.";
                s.Responses[(int)HttpStatusCode.NotFound] = "Not found.";
                s.Responses[(int)HttpStatusCode.BadRequest] = "Invalid input.";
                s.Responses[(int)HttpStatusCode.InternalServerError] = "Server Error.";
            });
        }
        public override async Task HandleAsync(DeleteContactRequest request, CancellationToken ct)
        {
            DeleteContactCommand command = request.Adapt<DeleteContactCommand>();
            var result = await _mediator.Send(command, ct);
            if (result.IsSuccess)
                await SendNoContentAsync(ct);
            else
                await SendNotFoundAsync(ct);
        }
    }
}
