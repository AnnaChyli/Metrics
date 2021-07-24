using System;
using System.Threading.Tasks;

namespace Metrics.Domain.Common
{
    public interface IMetrixReportRepository
    {
        Task StoreAsync(MetrixReport report);
    }
}
