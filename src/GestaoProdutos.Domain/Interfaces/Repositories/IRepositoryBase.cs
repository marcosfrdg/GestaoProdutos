using System.Collections.Generic;

namespace GestaoProdutos.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T>
    {
        T GetById(int id);
        IEnumerable<T> Get();
        IEnumerable<T> GetPaged(int page, int pageSize);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
