using Cifo.Model.Entity;
using Google.Cloud.Firestore;
using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifo.Service.Repository
{
    public class FirebaseDbRepository<T> : IRepository<T>, IDocumentCollectionContext<T> where T : Entity
    {
        private readonly FirestoreDb _db;
        public string CollectionName { get; }
        public FirebaseDbRepository(FirestoreDb db) => _db = db;

        public async Task<T> AddAsync(T entity)
        {
            entity.Id = GenerateId(entity);
            var docRef = _db.Collection(typeof(T).Name).Document(entity.Id);
            await docRef.SetAsync(entity).ConfigureAwait(false);
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public string GenerateId(T entity) => Guid.NewGuid().ToString();

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public PartitionKey ResolvePartitionKey(string entityId)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
