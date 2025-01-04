using MediatR;
using Report.API.Reports.GetReport;

namespace Report.API.Reports.CreateReport
{
    public class CreateContactEndpoint(IMediator _mediator) : Endpoint<CreateReportRequest, CreateReportResponse>
    {
        public override void Configure()
        {
            Post(CreateReportRequest.Route);
            AllowAnonymous();

            Description(b => b
                .Accepts<CreateReportRequest>("application/json")
                .Produces<CreateReportResponse>((int)HttpStatusCode.Created, "application/json")
                .Produces<CreateReportResponse>((int)HttpStatusCode.BadRequest, "application/json")
                .ProducesProblemFE<ValidationErrorResponse>((int)HttpStatusCode.BadRequest, "application/json")
                .ProducesProblemFE<BuildingBlocks.Responses.ErrorResponse>((int)HttpStatusCode.InternalServerError, "application/json"),
                 clearDefaults: true);

            Summary(s =>
            {
                s.ExampleRequest = new CreateReportRequest { Id = Guid.NewGuid() };
                s.Summary = "Create a report.";
                s.Description = "This endpoint allows you to create a report request.";
                s.Responses[(int)HttpStatusCode.Created] = "The report request was created successfully.";
                s.Responses[(int)HttpStatusCode.BadRequest] = "Invalid input";
                s.Responses[(int)HttpStatusCode.InternalServerError] = "Server Error.";
            });

        }
        public override async Task HandleAsync(CreateReportRequest request, CancellationToken ct)
        {
            CreateReportCommand command = request.Adapt<CreateReportCommand>();
            var result = await _mediator.Send(command, ct);
            if (!result.IsCreated)
                await SendAsync(new CreateReportResponse(null, $"The report with the id : '{request.Id}' already requested"), (int)HttpStatusCode.BadRequest, ct);
            else
            {
                var contactUrl = $"{CreateReportRequest.Route}/{result.Id}";
                CreateReportResponse response = result.Adapt<CreateReportResponse>();
                await SendCreatedAtAsync(contactUrl, response.Id, response, true, ct);
            }
        }
    }
}
