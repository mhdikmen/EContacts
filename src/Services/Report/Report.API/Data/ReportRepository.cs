using BuildingBlocks.Pagination;
using MongoDB.Driver;

namespace Report.API.Data
{
    public class ReportRepository : IReportRepository
    {
        private readonly IMongoCollection<Models.Report> _reports;
        public ReportRepository(IMongoClient mongoClient, IConfiguration configuration)
        {
            var databaseName = configuration.GetValue<string>("MongoDbSettings:DatabaseName");
            var database = mongoClient.GetDatabase(databaseName);
            _reports = database.GetCollection<Models.Report>("Reports");
        }

        public async Task<IEnumerable<Models.Report>> GetAllReportsAsync()
        {
            return await _reports.Find(_ => true).ToListAsync();
        }

        public async Task<Models.Report?> GetReportByIdAsync(Guid id)
        {
            return await _reports.Find(report => report.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateReportAsync(Models.Report report)
        {
            await _reports.InsertOneAsync(report);
        }

        public async Task UpdateReportAsync(Guid id, Models.Report report)
        {
            await _reports.ReplaceOneAsync(r => r.Id == id, report);
        }

        public async Task<PaginatedResult<Models.Report>> GetReportsPaginatedAsync(int pageIndex, int pageSize)
        {
            var totalRecords = await _reports.CountDocumentsAsync(_ => true);
            var pageCount = (long)Math.Ceiling((double)totalRecords / pageSize);

            var data = await _reports.Find(_ => true)
                                      .SortBy(report => report.RequestedDate)
                                      .Skip((pageIndex - 1) * pageSize)
                                      .Limit(pageSize)
                                      .ToListAsync();

            return new PaginatedResult<Models.Report>(
                pageIndex: pageIndex,
                pageSize: pageSize,
                count: totalRecords,
                pageCount: pageCount,
                data: data
            );
        }
    }
}