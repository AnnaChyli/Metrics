using System;
using System.Collections.Generic;
using Metrics.Domain.Common;

namespace Metrics.Domain.Tests.Mocks
{
    public class MockGitRepository : IGitRepository
    {
        public MockGitRepository(GitRepositoryConfiguration configuration, bool valid = false) {
            Configuration = configuration;
            IsValid = valid;
        }

        public bool IsValid { get; } = false;

        public GitRepositoryConfiguration Configuration { get; }

        public IList<CommitDescription> Synchronize() {
            throw new NotImplementedException();
        }

        public void EnsureClone() {
            if(!IsValid)
            {
                throw new RepositoryInitializationException("Repository cannot be intialized");
            }
        }
    }
}
