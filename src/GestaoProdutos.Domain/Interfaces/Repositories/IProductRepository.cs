using GestaoProdutos.Domain.Entities;
using System.Collections.Generic;

namespace GestaoProdutos.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        // Métodos específicos para o repositório de produtos...
        IEnumerable<Product> GetPagedProducts(int page, int pageSize, string filter, out int totalRecords);
        IEnumerable<Product> GetPagedProducts(string filter);
        void SoftDelete(int productId);
    }
}
