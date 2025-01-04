using BuildingBlocks.Enums;
using Report.API.Models;

namespace Report.API.Dtos.ReportDtos
{
    public record ReportDto
    {
        public Guid Id { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public ReportState State { get; set; }
        public ICollection<ReportDetail> ReportDetails { get; set; }
    }
}
