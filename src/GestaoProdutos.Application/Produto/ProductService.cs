using AutoMapper;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Exceptions;
using GestaoProdutos.Domain.Interfaces.Repositories;
using System.Collections.Generic;

namespace GestaoProdutos.Application.Produto
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public ProductDto GetProductById(int productId)
        {
            var product = _productRepository.GetById(productId);
            return _mapper.Map<ProductDto>(product);
        }

        public IEnumerable<ProductDto> GetPagedProducts(int page, int pageSize, string filter, out int totalRecords, out int currentPage)
        {
            var products = _productRepository.GetPagedProducts(page, pageSize, filter, out totalRecords);

            currentPage = page;

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public IEnumerable<ProductDto> GetPagedProducts(string filter)
        {
            var products = _productRepository.GetPagedProducts(filter);

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public void AddProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _productRepository.Add(product);
        }

        public void UpdateProduct(ProductDto productDto)
        {
            var existingProduct = _productRepository.GetById(productDto.Id)
                ?? throw new NotFoundException("Produto não encontrado");

            _mapper.Map(productDto, existingProduct);
            _productRepository.Update(existingProduct);
        }

        public void SoftDeleteProduct(int productId)
        {
            _productRepository.SoftDelete(productId);
        }
    }

}
