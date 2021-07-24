using System;
using System.IO;
using Metrics.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Metrics.Data.Tests
{
    [TestClass]
    public class GitRepositoryTests
    {
        private string remoteUrl = "github.com";
        private string workDirectory = "workdir";
        private string login = "git_login";
        private string pwd = "git_pswd";
        private string branch = "master";

        [Ignore]
        [TestMethod]
        public void When_EnsureInitialize_ValidRepoIsCreated()
        {
            // arrange 
            var credentials = new Credentials(login, pwd);
            var config = new GitRepositoryConfiguration(
                remoteUrl,
                workDirectory,
                branch,
                credentials);


            var repo = new GitRepository(config);

            // act
            repo.EnsureClone();

            // assert
            Assert.IsTrue(repo.IsValid);
            Directory.Delete(workDirectory, true);
        }
    }
}
