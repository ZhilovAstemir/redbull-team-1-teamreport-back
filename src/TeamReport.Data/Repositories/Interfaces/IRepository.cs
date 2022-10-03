using System.Runtime.InteropServices;

namespace redbull_team_1_teamreport_back.Data.Repositories.Interfaces;

public interface IRepository<TEntity>
{
    public Task<TEntity> Create(TEntity entity);
    public Task<TEntity?> Read(int entityId);
    public Task<bool> Update(TEntity entity);
    public Task<bool> Delete(int entityId);
    public Task<List<TEntity>> ReadAll();
}