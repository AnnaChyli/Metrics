using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Metrics.Data.Common;
using Metrics.Data.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Metrics.Data.Tests
{
    [TestClass]
    public class DataContextTests
    {
        private IDataContext _context;
        private const string dbPath = @"database.db";

        [TestInitialize]
        public void BeforeEachTest()
        {
            _context = new DataContext(dbPath);
        }

        [TestCleanup]
        public void AfterEachTest()
        {
            _context.Dispose();
            File.Delete(dbPath);
        }

        [TestMethod]
        public async Task InsertAsyncTest()
        {
            Guid id = Guid.NewGuid();
            Person person = new()
            {
                Id = id,
                FirstName = "Anna",
                LastName = "Chylikina"
            };

            var insertedId = await _context.InsertAsync(person);
            Assert.AreEqual(id, insertedId);
        }


        [TestMethod]
        public async Task UpdateAsyncTest()
        {
            Guid id = Guid.NewGuid();
            Person person = new()
            {
                Id = id,
                FirstName = "Anna",
                LastName = "Chylikina"
            };

            var _ = await _context.InsertAsync(person);

            person.LastName = "New Last Name";
            bool success = await _context.UpdateAsync(person);

            Assert.IsTrue(success);
        }

        [TestMethod]
        public async Task DeleteAsyncSuccessTest()
        {
            Guid id = Guid.NewGuid();
            Person person = new()
            {
                Id = id,
                FirstName = "Anna",
                LastName = "Chylikina"
            };

            var _ = await _context.InsertAsync(person);
            bool success = await _context.DeleteAsync(person);

            Assert.IsTrue(success);
        }

        [TestMethod]
        public async Task DeleteAsyncFailedTest()
        {
            Person anotherInstance = new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Anna",
                LastName = "Chylikina"
            };

            bool success = await _context.DeleteAsync(anotherInstance);

            Assert.IsFalse(success);
        }

        [TestMethod]
        public async Task GetByIdAsync()
        {
            Guid id = Guid.NewGuid();
            Person person = new()
            {
                Id = id,
                FirstName = "Anna",
                LastName = "Chylikina"
            };

            var _ = await _context.InsertAsync(person);
            Person result = await _context.GetByIdAsync<Person>(id);

            Assert.AreEqual(id, result.Id);
        }

        [TestMethod]
        public async Task FindAsyncFound()
        {
            Guid id = Guid.NewGuid();
            Person person = new()
            {
                Id = id,
                FirstName = "Anna",
                LastName = "Chylikina"
            };

            var _ = await _context.InsertAsync(person);
            var result = await _context.FindAsync<Person>(p => p.FirstName == person.FirstName);

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public async Task FindAsyncNotFound()
        {
            Guid id = Guid.NewGuid();
            Person person = new()
            {
                Id = id,
                FirstName = "Anna",
                LastName = "Chylikina"
            };

            var _ = await _context.InsertAsync(person);
            var result = await _context.FindAsync<Person>(p => p.FirstName == Guid.NewGuid().ToString());

            Assert.AreEqual(0, result.Count());
        }
    }
}
