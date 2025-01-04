using BuildingBlocks.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Report.API.Models
{
    public class Report 
    {
        public Report()
        {
            Id = Guid.NewGuid();
            RequestedDate = DateTime.UtcNow;
            State = ReportState.Preparing;
            ReportDetails = [];
        }

        public Report(Guid id)
        {
            Id = id;
            RequestedDate = DateTime.UtcNow;
            State = ReportState.Preparing;
            ReportDetails = [];
        }

        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public ReportState State { get; set; }
        public ICollection<ReportDetail> ReportDetails { get; set; }

        public void AddReportDetail(ReportDetail reportDetail)
        {
            ReportDetails.Add(reportDetail);
        }
    }
}
