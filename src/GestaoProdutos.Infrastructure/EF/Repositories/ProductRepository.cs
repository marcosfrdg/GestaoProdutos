using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Enums;
using GestaoProdutos.Domain.Exceptions;
using GestaoProdutos.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoProdutos.Infrastructure.EF.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {

        public ProductRepository(DataContext context) : base(context) { }

        public IEnumerable<Product> GetPagedProducts(string filter)
        {
            var query = Get().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(p =>
                    p.Description.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                    p.SupplierDescription.Contains(filter, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToList();
        }

        public IEnumerable<Product> GetPagedProducts(int page, int pageSize, string filter, out int totalRecords)
        {
            var query = Get().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(p =>
                    p.Description.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                    p.SupplierDescription.Contains(filter, StringComparison.OrdinalIgnoreCase));
            }

            totalRecords = query.Count();

            var pagedProducts = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return pagedProducts;
        }

        public void SoftDelete(int productId)
        {
            var product = GetById(productId);

            if (product == null)
            {
                throw new NotFoundException("Produto não encontrado");
            }

            product.Status = ProductStatus.Inativo;
            Update(product);
        }
    }

}
