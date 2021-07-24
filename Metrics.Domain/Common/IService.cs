using System;
namespace Metrics.Domain.Common
{
    public interface IService
    {
        void Start();
        void Stop();
        bool Running { get; }
    }
}
