using Cifo.Model.Entity;
using Microsoft.Azure.Documents;

namespace Cifo.Service.Repository
{
    public interface IDocumentCollectionContext<in TEntity> where TEntity : Entity
    {
        string CollectionName { get; }
        string GenerateId(TEntity entity);
        PartitionKey ResolvePartitionKey(string entityId);
    }
}
