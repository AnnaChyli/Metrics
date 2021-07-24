using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Metrics.Domain.Common;

namespace Metrics.Domain
{
    public class MetrixMonitor : IService, IDisposable
    {
        private readonly IGitRepository _gitRepo;
        private readonly TimeSpan _monitoringInterval;
        private readonly IMetrixGenerator _generator;
        private IDisposable _subscription;

        private readonly object _sync = new();
        private bool _disposed = false;

        
        public MetrixMonitor(IGitRepository repo, IMetrixGenerator generator, TimeSpan monitoringInterval) {
            _gitRepo = repo;
            _generator = generator;
            _monitoringInterval = monitoringInterval;
        }

        ~MetrixMonitor() => Dispose(false);

        public bool Running { get; private set; }

        public void Start()
        {
            // _sync cannot be nullify by external class
            lock (_sync)
            {
                if (Running)
                   return;

                _gitRepo.EnsureClone();
                _subscription = Observable
                    .Interval(_monitoringInterval)
                    .Subscribe(x => Process());

                Running = true;
            }
        }

        public void Stop()
        {
            lock (_sync)
            {
                if (Running) {
                    _subscription.Dispose();
                    _subscription = null;
                    Running = false;
                }
            }
        }

        protected virtual void Process()
        {
            IList<CommitDescription> commits = _gitRepo.Synchronize();

            IEnumerable<Task> tasks =
                commits.Select(commit => _generator.GenerateAsync(commit, _gitRepo.Configuration.WorkingDirectory));

            Task.WaitAll(tasks.ToArray());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _subscription?.Dispose();
            }

            _disposed = true;
        }
    }
}
