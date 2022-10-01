using System.Runtime.InteropServices;

namespace redbull_team_1_teamreport_back.Data.Repositories.Interfaces;

public interface IRepository<TEntity>
{
    public TEntity Create(TEntity entity);
    public TEntity? Read(int entityId);
    public bool Update(TEntity entity);
    public bool Delete(int entityId);
    public List<TEntity> ReadAll();
}