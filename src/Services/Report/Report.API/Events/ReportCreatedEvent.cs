namespace Report.API.Events;
public record ReportCreatedEvent(Guid id)
{
    public Guid ReportId { get; set; } = id;
}
