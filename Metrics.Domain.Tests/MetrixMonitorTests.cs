using Microsoft.VisualStudio.TestTools.UnitTesting;
using Metrics.Domain;
using Metrics.Domain.Tests.Mocks;
using System.Threading.Tasks;
using System;
using Metrics.Domain.Common;

namespace Metrics.Domain.Tests
{
    [TestClass]
    public class MetrixMonitorTests
    {
        string remoteUrl = "";
        string localDirectory = "";
        string login = "";
        string pwd = "";
        string branch = "";

        [TestMethod]
        public void StartMonitor_Sucess()
        {
            var credentials = new Credentials(login, pwd);
            var configuration = new GitRepositoryConfiguration(remoteUrl, localDirectory, branch, credentials);
            var repository = new MockGitRepository(configuration, true);
            TimeSpan interval = TimeSpan.FromSeconds(5);

            IMetrixGenerator metrixGenerator = new MockMetrixGenerator(null);

            using MetrixMonitor monitor = new MetrixMonitor(repository, metrixGenerator, interval);
            monitor.Start();
            Assert.IsTrue(monitor.Running);
            monitor.Stop();
        }

        [TestMethod]
        public void StartMonitor_RepositoryFailedToIntialze_MonitorIsNotRunning()
        {
            string remoteUrl = "";
            string localDirectory = "";
            string login = "";
            string pwd = "";
            string branch = "";

            var credentials = new Credentials(login, pwd);
            var configuration = new GitRepositoryConfiguration(remoteUrl, localDirectory, branch, credentials);
            var repository = new MockGitRepository(configuration, false);
            TimeSpan interval = TimeSpan.FromSeconds(5);
            using var monitor = new MetrixMonitor(repository, null, interval);
            Assert.ThrowsException<RepositoryInitializationException>(() => monitor.Start());
            Assert.IsFalse(monitor.Running);
        }

    }
}
