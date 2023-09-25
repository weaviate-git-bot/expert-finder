namespace ExpertFinder.Weaviate;

public interface IVectorDatabaseClient
{
    Task CreateObjectAsync(VectorObject data);
    Task DeleteObjectAsync(string className, Guid objectId);
    Task<VectorObject?> GetObjectAsync(string className, Guid objectId);
    Task<bool> ObjectExistsAsync(string className, Guid objectId);
    Task UpdateObjectAsync(VectorObject data);
}
