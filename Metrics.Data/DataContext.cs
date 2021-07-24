using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LiteDB;
using Metrics.Data.Common;
using Metrics.Domain.Common;

namespace Metrics.Data
{
    //Dispose Patern: https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
    public class DataContext : IDataContext
    {
        private bool _disposed = false;
        private readonly LiteDatabase _database;

        public DataContext(string dbPath)
        {
            Path = dbPath;
            _database = new LiteDatabase(dbPath);
        }

        ~DataContext() => Dispose(false);

        public string Path { get; }

        public async Task<Guid> InsertAsync<T>(T obj) where T : IEntity 
        {
            ILiteCollection<T> collection = await GetCollectionAsync<T>();
            return collection.Insert(obj);
        }

        public async Task<bool> UpdateAsync<T>(T obj) where T : IEntity 
        {
            ILiteCollection<T> collection = await GetCollectionAsync<T>();
            return collection.Update(obj);
        }

        public async Task<bool> DeleteAsync<T>(T obj) where T : IEntity
        {
            ILiteCollection<T> collection = await GetCollectionAsync<T>();
            return collection.Delete(obj.Id);
        }

        public async Task<bool> DeleteAsync<T>(Guid id) where T : IEntity
        {
            ILiteCollection<T> collection = await GetCollectionAsync<T>();
            return collection.Delete(id);
        }

        public async Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> whereClause) where T : IEntity
        {
            ILiteCollection<T> collection = await GetCollectionAsync<T>();
            return collection.Find(whereClause);
        }

        public async Task<T> GetByIdAsync<T>(Guid id) where T : IEntity
        {
            ILiteCollection<T> collection = await GetCollectionAsync<T>();
            return collection.FindById(id);
        }

        public Task UploadAsync(string id, string filename, Stream data)
        {
            return Task.Run(() => _database.FileStorage.Upload(id, filename, data));
        }

        public Task DownloadAsync(string id, Stream data)
        {
            return Task.Run(() => _database.FileStorage.Download(id, data));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _database?.Dispose();
            }

            _disposed = true;
        }

        private Task<ILiteCollection<T>> GetCollectionAsync<T>() where T : IEntity
        {
            return Task.FromResult(_database.GetCollection<T>(nameof(T)));
        }
    }
}
