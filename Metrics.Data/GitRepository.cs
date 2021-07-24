using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using Metrics.Domain;
using Metrics.Domain.Common;

namespace Metrics.Data
{
    public class GitRepository : IGitRepository
    {
        public GitRepository(GitRepositoryConfiguration configuration)
        {
            Configuration = configuration;
        }

        public bool IsValid => Repository.IsValid(Configuration.WorkingDirectory);

        public GitRepositoryConfiguration Configuration { get; }

        public void EnsureClone()
        {
            if (DirectoryDoesNotExist(Configuration.WorkingDirectory))
                CloneRemoteRepository(Configuration);

            CheckRepositoryIsValid(Configuration.WorkingDirectory);
        }

        public IList<CommitDescription> Synchronize()
        {
            using Repository repo = new(Configuration.WorkingDirectory);
            Remote remote = repo.Network.Remotes.First();
            FetchOptions fetchOptions = CreateFetchOptions(Configuration);

            Commands.Fetch(repo, remote.Name, Array.Empty<string>(), fetchOptions, null);

            Branch trackingBranch = repo.Head.TrackedBranch;
            CommitFilter commitFilter = CreateCommitFilter(repo, trackingBranch);

            ICommitLog log = repo.Commits.QueryBy(commitFilter);
            List<CommitDescription> listOfUnsyncCommit = CovertCommitLogToCommitDescriptions(log);

            Merge();
            return listOfUnsyncCommit;
        }

        private static List<CommitDescription> CovertCommitLogToCommitDescriptions(ICommitLog log)
        {
            IEnumerable<CommitDescription> descriptions = log.Select(commit => new CommitDescription
            {
                Sha = commit.Sha,
                Author = commit.Author.Name,
                CreateDate = commit.Author.When.DateTime
            });

            return descriptions.ToList();
        }

        private static CommitFilter CreateCommitFilter(Repository repo, Branch trackingBranch)
        {
            return new()
            {
                IncludeReachableFrom = trackingBranch.Tip.Id,
                ExcludeReachableFrom = repo.Head.Tip.Id
            };
        }

        private static FetchOptions CreateFetchOptions(GitRepositoryConfiguration configuration)
        {
            return new()
            {
                CredentialsProvider = CreateCredentialsHandler(configuration)
            };
        }

        private void Merge()
        {
            Directory.Delete(Configuration.WorkingDirectory);
            CloneRemoteRepository(Configuration);
        }

        private bool DirectoryDoesNotExist(string path) 
        {
            return !Directory.Exists(path);
        }

        private void CheckRepositoryIsValid(string path) 
        {
            if (!Repository.IsValid(path))
            {
                throw new RepositoryInitializationException($"Repository cannot be intialized. Please check the folder {path}");
            }
        }

        private static void CloneRemoteRepository(GitRepositoryConfiguration configuration) 
        {
            CloneOptions cloneOptions = new()
            {
                BranchName = configuration.Branch,
                CredentialsProvider = CreateCredentialsHandler(configuration)
            };

            Repository.Clone(configuration.Url, configuration.WorkingDirectory, cloneOptions);
        }

        private static CredentialsHandler CreateCredentialsHandler(GitRepositoryConfiguration configuration)
        {
            UsernamePasswordCredentials credentials = new()
            {
                Username = configuration.Credentials.Login,
                Password = configuration.Credentials.Pwd
            };

            return (_url, _user, _cred) => credentials;
        }
    }
}
