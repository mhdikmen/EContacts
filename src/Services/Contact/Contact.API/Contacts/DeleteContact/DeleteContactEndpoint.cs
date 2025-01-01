using BuildingBlocks.Consts;
using Contact.API.Contacts.DeleteContact;
using MediatR;

namespace Contact.API.Contacts.CreateContact
{
    public class DeleteContactEndpoint(IMediator _mediator) : Endpoint<DeleteContactRequest, DeleteContactResponse>
    {
        public override void Configure()
        {
            Delete(DeleteContactRequest.Route);
            AllowAnonymous();
            Summary(s =>
            {
                s.ExampleRequest = new DeleteContactRequest { Id = Guid.NewGuid() };
            });
        }
        public override async Task HandleAsync(DeleteContactRequest request, CancellationToken ct)
        {
            DeleteContactCommand command = request.Adapt<DeleteContactCommand>();
            var result = await _mediator.Send(command, ct);
            var response = result.IsSuccess ? new DeleteContactResponse(System.Net.HttpStatusCode.NoContent, Messages.DeletedMessage) : new DeleteContactResponse(System.Net.HttpStatusCode.NotFound, Messages.NotFoundMessage);
            await SendAsync(response, response.HttpStatusCode, ct);
        }
    }
}
