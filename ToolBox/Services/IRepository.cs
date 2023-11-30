namespace ToolBox.Services;

public interface IRepository<Tkey, TEntity>
{
    IEnumerable<TEntity> GetAll();
    TEntity? Get(Tkey id);
    TEntity? Create(TEntity entity);
    bool Update(TEntity entity);
    bool Delete(TEntity entity);
}

    
