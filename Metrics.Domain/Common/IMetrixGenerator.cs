using System;
using System.Threading.Tasks;

namespace Metrics.Domain.Common
{
    public interface IMetrixGenerator
    {
        Task GenerateAsync(CommitDescription descriptor, string workDir);
    }
}
