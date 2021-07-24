using System;
using System.Collections.Generic;

namespace Metrics.Domain
{
    public interface IGitRepository
    {
        void EnsureClone();
        bool IsValid { get; }
        GitRepositoryConfiguration Configuration { get; }
        IList<CommitDescription> Synchronize();
    }
}
