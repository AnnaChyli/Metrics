using System;
using System.IO;
using System.Threading.Tasks;
using Metrics.Data.Common;
using Metrics.Domain;
using Metrics.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Metrics.Data.Tests
{
    [TestClass]
    public class MetrixReportRepositoryTests
    {
        [TestMethod]
        public async Task Store()
        {
            string dbPath = Guid.NewGuid() + ".db";
            using DataContext context = new(dbPath);
            IMetrixReportRepository storage = new MetrixReportRepository(context);

            Guid reportId = Guid.NewGuid();
            var report = new MetrixReport
            {
                Id = reportId,
                Sha = "bfabdfg",
                Data = "some xml data",
            };

            await storage.StoreAsync(report);

            MetrixReport result = await context.GetByIdAsync<MetrixReport>(reportId);
            Assert.AreEqual(reportId, result.Id);
            
            File.Delete(dbPath);
        }
    }
}
