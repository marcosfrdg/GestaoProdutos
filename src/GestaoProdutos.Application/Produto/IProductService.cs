using System.Collections.Generic;

namespace GestaoProdutos.Application.Produto
{
    public interface IProductService
    {
        ProductDto GetProductById(int productId);
        IEnumerable<ProductDto> GetPagedProducts(int page, int pageSize, string filter, out int totalRecords, out int currentPage);
        IEnumerable<ProductDto> GetPagedProducts(string filter);
        void AddProduct(ProductDto productDto);
        void UpdateProduct(ProductDto productDto);
        void SoftDeleteProduct(int productId);
    }
}
