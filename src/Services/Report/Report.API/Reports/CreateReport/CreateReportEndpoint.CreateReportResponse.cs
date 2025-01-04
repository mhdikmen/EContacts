namespace Report.API.Reports.CreateReport
{
    public record CreateReportResponse(Guid? Id, string Message = "Report request was successfully created");
}
