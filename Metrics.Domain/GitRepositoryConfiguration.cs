using System;
namespace Metrics.Domain
{
    public class GitRepositoryConfiguration
    {
        public GitRepositoryConfiguration(string url, string localDirectory, string branch, Credentials credentials)
        {
            Url = url;
            WorkingDirectory = localDirectory;
            Credentials = credentials;
            Branch = branch;
        }

        public string Url { get; }
        public string Branch { get; }
        public string WorkingDirectory { get; }
        public Credentials Credentials { get; }

    }
}
