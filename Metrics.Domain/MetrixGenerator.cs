using System;
using System.IO;
using System.Threading.Tasks;
using Metrics.Domain.Common;

namespace Metrics.Domain
{
   
    public class MetrixGenerator : IMetrixGenerator
    {
        private readonly IMetrixReportRepository _metrixReportRepository;

        public MetrixGenerator(IMetrixReportRepository metrixReportRepository)
        {
            this._metrixReportRepository = metrixReportRepository;
        }

        // we should push a valid repository over parammeter
        public Task GenerateAsync(CommitDescription descriptor, string workdir)
        {
            //string name = GenerateName();
            // CreateDirectory(name);
            // CloneSourceRepository()
            // Checkout(descritor.Hash)

            // generate report here

            MetrixReport report = new();

            // save report to database
            return _metrixReportRepository.StoreAsync(report);
        }
    }
}
