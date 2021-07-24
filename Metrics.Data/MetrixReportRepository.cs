using System;
using System.Threading.Tasks;
using Metrics.Domain;
using Metrics.Domain.Common;

namespace Metrics.Data
{
    public class MetrixReportRepository : IMetrixReportRepository
    {
        private readonly DataContext _context;

        public MetrixReportRepository(DataContext context)
        {
            _context = context;
        }

        public Task StoreAsync(MetrixReport report)
        {
            return _context.InsertAsync(report);
        }
    }
}
