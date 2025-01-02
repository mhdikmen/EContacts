using BuildingBlocks.Consts;
using Contact.API.Contacts.DeleteContact;
using MediatR;

namespace Contact.API.Contacts.CreateContact
{
    public class DeleteContactEndpoint(IMediator _mediator) : Endpoint<DeleteContactRequest>
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
            if(result.IsSuccess)
                await SendNoContentAsync(ct);
            else
                await SendNotFoundAsync(ct);
        }
    }
}
