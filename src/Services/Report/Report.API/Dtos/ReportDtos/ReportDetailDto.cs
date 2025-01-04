namespace Report.API.Dtos.ReportDtos
{
    public record ReportDetailDto
    {
        public string Location { get; set; } = default!;
        public long ContactCount { get; set; }
        public long PhoneNumberCount { get; set; }
    }
}
