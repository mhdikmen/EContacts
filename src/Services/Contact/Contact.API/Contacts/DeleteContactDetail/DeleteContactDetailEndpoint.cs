using Contact.API.Contacts.GetContact;
using MediatR;

namespace Contact.API.Contacts.DeleteContactDetail
{
    public class DeleteContactDetailEndpoint(IMediator _mediator) : Endpoint<DeleteContactDetailRequest>
    {
        public override void Configure()
        {
            Delete(DeleteContactDetailRequest.Route);
            AllowAnonymous();

            Description(b => b
                .Accepts<DeleteContactDetailRequest>()
                .Produces((int)HttpStatusCode.NoContent)
                .Produces((int)HttpStatusCode.NotFound)
                .ProducesProblemFE<ValidationErrorResponse>((int)HttpStatusCode.BadRequest, "application/json")
                .ProducesProblemFE<BuildingBlocks.Responses.ErrorResponse>((int)HttpStatusCode.InternalServerError, "application/json"),
                 clearDefaults: true);

            Summary(s =>
            {
                s.ExampleRequest = new DeleteContactDetailRequest { ContactId = Guid.NewGuid(), ContactDetailId = Guid.NewGuid() };
                s.Summary = "Delete a contact detail.";
                s.Description = "This endpoint allows you to delete a contact detail.";
                s.Responses[(int)HttpStatusCode.NoContent] = "The contact detail was deleted successfully.";
                s.Responses[(int)HttpStatusCode.Created] = "Invalid input.";
                s.Responses[(int)HttpStatusCode.InternalServerError] = "Server Error.";
            });
        }
        public override async Task HandleAsync(DeleteContactDetailRequest request, CancellationToken ct)
        {
            DeleteContactDetailCommand command = request.Adapt<DeleteContactDetailCommand>();
            var result = await _mediator.Send(command, ct);
            if (result.IsSuccess)
                await SendNoContentAsync(ct);
            else
                await SendNotFoundAsync(ct);
        }
    }
}
