using System;
namespace Metrics.Domain.Common
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}
