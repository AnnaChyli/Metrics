using System;
using Metrics.Domain.Common;

namespace Metrics.Domain
{
    public class MetrixReport : IEntity
    {
        public Guid Id { get; set; }
        public string Sha { get; set; }
        public string Data { get; set; }
    }
}
