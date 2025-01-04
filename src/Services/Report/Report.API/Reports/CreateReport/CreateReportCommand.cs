namespace Report.API.Reports.CreateReport
{
    public record CreateReportCommand(Guid Id) : BuildingBlocks.CQRS.ICommand<CreateReportResult>;
}
