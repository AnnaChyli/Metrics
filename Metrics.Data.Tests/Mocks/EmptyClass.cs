using System;
using Metrics.Domain.Common;

namespace Metrics.Data.Tests.Mocks
{
    public class Person : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
