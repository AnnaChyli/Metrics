using System;
namespace Metrics.Domain
{
    public class CommitDescription
    {
        public string Sha { get; set; }
        public string Author { get; set; }
        public DateTime CreateDate { get; set; }
        public string ShortSha => Sha.Substring(0, 7);
    }
}
