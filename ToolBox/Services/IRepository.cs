namespace ToolBox.Services;

public interface IRepository<TKey, TEntity>
{
    IEnumerable<TEntity> GetAll();
    TEntity? Get(TKey id);
    TEntity? Create(TEntity entity);
    bool Update(TEntity entity);
    bool Delete(TKey id);
}

    
