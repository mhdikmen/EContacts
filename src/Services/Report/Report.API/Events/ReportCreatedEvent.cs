namespace Report.API.Events;
public record ReportCreatedEvent
{
    public ReportCreatedEvent(Guid reportId)
    {
        ReportId = reportId;
    }
    public Guid ReportId { get; set; }
}
