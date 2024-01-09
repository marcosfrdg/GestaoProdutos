using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GestaoProdutos.Infrastructure.EF.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity<int>
    {
        private readonly DataContext _context;

        public RepositoryBase(DataContext context)
        {
            _context = context;
        }

        public T GetById(int id)
        {
            return _context.Set<T>().FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> Get()
        {
            return _context.Set<T>().ToList();
        }

        public IEnumerable<T> GetPaged(int page, int pageSize)
        {
            return _context.Set<T>().Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Set<T>().FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }

    }
}
