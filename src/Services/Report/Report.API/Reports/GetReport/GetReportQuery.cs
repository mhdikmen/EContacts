namespace Report.API.Reports.GetReport
{
    public record GetReportQuery(Guid Id) : BuildingBlocks.CQRS.IQuery<GetReportResult>;
}
