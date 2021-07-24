using System;
using System.Threading.Tasks;
using Metrics.Domain.Common;

namespace Metrics.Domain.Tests.Mocks
{
    public class MockMetrixGenerator : IMetrixGenerator
    {
        private MetrixReport _report;

        public MockMetrixGenerator(MetrixReport report)
        {
            _report = report;
        }

        public Task GenerateAsync(CommitDescription descriptor, string workDir)
        {
            return Task.FromResult(_report);
        }
    }
}
