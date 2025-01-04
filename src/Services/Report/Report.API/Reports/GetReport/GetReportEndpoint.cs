using MediatR;

namespace Report.API.Reports.GetReport
{
    public class GetReportEndpoint(IMediator _mediator) : Endpoint<GetReportRequest, GetReportResponse>
    {
        public override void Configure()
        {
            Get(GetReportRequest.Route);
            AllowAnonymous();

            Description(b => b
                .Accepts<GetReportRequest>()
                .Produces<GetReportResponse>((int)HttpStatusCode.OK, "application/json")
                .ProducesProblemFE<ValidationErrorResponse>((int)HttpStatusCode.BadRequest, "application/json")
                .ProducesProblemFE<BuildingBlocks.Responses.ErrorResponse>((int)HttpStatusCode.InternalServerError, "application/json"),
                 clearDefaults: true);

            Summary(s =>
            {
                s.ExampleRequest = new GetReportRequest { Id = Guid.NewGuid() };
                s.Summary = "Gets a report.";
                s.Description = "This endpoint allows you to get a report.";
                s.Responses[(int)HttpStatusCode.OK] = "The report request was found successfully.";
                s.Responses[(int)HttpStatusCode.BadRequest] = "Invalid input.";
                s.Responses[(int)HttpStatusCode.InternalServerError] = "Server Error.";
            });

        }
        public override async Task HandleAsync(GetReportRequest request, CancellationToken ct)
        {
            GetReportQuery command = request.Adapt<GetReportQuery>();
            var result = await _mediator.Send(command, ct);
            if (!result.IsExists)
                await SendNotFoundAsync(ct);
            GetReportResponse response = result.Adapt<GetReportResponse>();
            await SendOkAsync(response, ct);
        }
    }
}
