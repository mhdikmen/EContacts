using BuildingBlocks.Enums;
using MediatR;

namespace Report.API.Reports.GetReports
{
    public class GetReportsEndpoint(IMediator _mediator) : Endpoint<GetReportsRequest, GetReportsResponse>
    {
        public override void Configure()
        {
            Get(GetReportsRequest.Route);
            AllowAnonymous();

            Description(b => b
                .Accepts<GetReportsRequest>()
                .Produces<GetReportsResponse>((int)HttpStatusCode.OK, "application/json")
                .Produces((int)HttpStatusCode.NotFound)
                .ProducesProblemFE<ValidationErrorResponse>((int)HttpStatusCode.BadRequest, "application/json")
                .ProducesProblemFE<BuildingBlocks.Responses.ErrorResponse>((int)HttpStatusCode.InternalServerError, "application/json"),
                 clearDefaults: true);

            Summary(s =>
            {
                s.ExampleRequest = new GetReportsRequest { PageIndex = 0, PageSize = 100 };
                s.Summary = "Gets reports with the details.";
                s.Description = string.Concat("This endpoint allows you to get reports.", "Report states are ", string.Join(" , ", Enum.GetValues<ReportState>().Select(e => string.Concat(((int)e).ToString(), " - ", e))));
                s.Responses[(int)HttpStatusCode.OK] = "Gets the list of reports.";
                s.Responses[(int)HttpStatusCode.Created] = "Invalid input.";
                s.Responses[(int)HttpStatusCode.InternalServerError] = "Server Error.";
            });
        }
        public override async Task HandleAsync(GetReportsRequest request, CancellationToken ct)
        {
            GetReportsQuery command = request.Adapt<GetReportsQuery>();
            var result = await _mediator.Send(command, ct);
            GetReportsResponse response = result.Adapt<GetReportsResponse>();
            await SendOkAsync(response, ct);
        }
    }
}
