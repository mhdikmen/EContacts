namespace Report.API.Models
{
    public class ReportDetail
    {
        public ReportDetail()
        {
            
        }
        public ReportDetail(string location, long contactCount, long phoneNumberCount)
        {
            Location = location;
            ContactCount = contactCount;
            PhoneNumberCount = phoneNumberCount;
        }

        public string Location { get; set; } = default!;
        public long ContactCount { get; set; }
        public long PhoneNumberCount { get; set; }
    }
}
