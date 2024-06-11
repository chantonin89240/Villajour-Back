using Villajour.Application.Chatbot.Interfaces;
using Villajour.Domain.Entities.Chatbot.Interfaces;

namespace Villajour.Persistence.Chatbot.Repositories;

/// <summary>
/// Defines the basic CRUD operations for a repository.
/// </summary>
public class Repository<T> : IRepository<T> where T : IStorageEntity
{
    /// <summary>
    /// The storage context.
    /// </summary>
    protected IStorageContext<T> StorageContext { get; set; }

    /// <summary>
    /// Initializes a new instance of the Repository class.
    /// </summary>
    public Repository(IStorageContext<T> storageContext)
    {
        StorageContext = storageContext;
    }

    /// <inheritdoc/>
    public Task CreateAsync(T entity)
    {
        if (string.IsNullOrWhiteSpace(entity.Id))
        {
            throw new ArgumentOutOfRangeException(nameof(entity), "Entity ID cannot be null or empty.");
        }

        return StorageContext.CreateAsync(entity);
    }

    /// <inheritdoc/>
    public Task DeleteAsync(T entity)
    {
        return StorageContext.DeleteAsync(entity);
    }

    /// <inheritdoc/>
    public Task<T> FindByIdAsync(string id, string? partition = null)
    {
        return StorageContext.ReadAsync(id, partition ?? id);
    }

    /// <inheritdoc/>
    public async Task<bool> TryFindByIdAsync(string id, string? partition = null, Action<T?>? callback = null)
    {
        try
        {
            var found = await FindByIdAsync(id, partition ?? id);

            callback?.Invoke(found);

            return true;
        }
        catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is KeyNotFoundException)
        {
            return false;
        }
    }

    /// <inheritdoc/>
    public Task UpsertAsync(T entity)
    {
        return StorageContext.UpsertAsync(entity);
    }
}
