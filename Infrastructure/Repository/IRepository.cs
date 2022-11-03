namespace Infrastructure.Repository
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> Get(Guid id);
        Task<TEntity> Add(TEntity data);
        void Update(TEntity data);
        Task<bool> Delete(Guid id);
        Task Save();

    }
}
