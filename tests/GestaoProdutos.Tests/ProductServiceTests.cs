using AutoMapper;
using FluentAssertions;
using GestaoProdutos.Application.AutoMapper;
using GestaoProdutos.Application.Produto;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Exceptions;
using GestaoProdutos.Domain.Interfaces.Repositories;
using Moq;
using Moq.AutoMock;
using System.Collections.Generic;
using Xunit;

namespace GestaoProdutos.Tests
{
    public class ProductServiceTests
    {
        private readonly ProductService _productService;
        private readonly AutoMocker _mocker;
        private readonly IMapper _mapper;

        public ProductServiceTests()
        {
            _mocker = new AutoMocker();

            var myProfile = _mocker.CreateInstance<AutoMapperConfig>();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            _mapper = new Mapper(configuration);
            _mocker.Use(_mapper);

            _productService = _mocker.CreateInstance<ProductService>();
        }

        [Fact]
        public void Deve_ObterProdutoPorId_QuandoIdProdutoValido_RetornaProductDto()
        {
            // Arrange
            var productDto = ProductFaker.GenerateFakeProduct().Generate();

            _mocker.GetMock<IProductRepository>()
                .Setup(x => x.GetById(productDto.Id))
                .Returns(_mapper.Map<Product>(productDto));

            // Act
            var result = _productService.GetProductById(productDto.Id);

            // Assert
            result.Should().BeEquivalentTo(_mapper.Map<Product>(productDto));
        }

        [Fact]
        public void Deve_ObterProdutosPaginados_QuandoParametrosValidos_RetornaListaProductDto()
        {
            // Arrange
            var page = 1;
            var pageSize = 10;
            var filter = "AlgumFiltro";
            var totalRecords = 20;

            var productsList = ProductFaker.GenerateFakeProduct().Generate(10);

            _mocker.GetMock<IProductRepository>()
                .Setup(x => x.GetPagedProducts(page, pageSize, filter, out totalRecords))
                .Returns(productsList);

            // Act
            var result = _productService.GetPagedProducts(page, pageSize, filter, out totalRecords, out int currentPage);

            // Assert
            result.Should().BeEquivalentTo(_mapper.Map<List<ProductDto>>(productsList));
            currentPage.Should().Be(page);
        }

        [Fact]
        public void Deve_ObterProdutosPaginados_QuandoApenasFiltro_RetornaListaProductDto()
        {
            // Arrange
            var filter = "AlgumFiltro";

            var productDto = ProductFaker.GenerateFakeProductDto().Generate();

            var productsList = ProductFaker.GenerateFakeProduct().Generate(10);
            var expectedProductDtoList = _mapper.Map<List<ProductDto>>(productsList);

            _mocker.GetMock<IProductRepository>()
                .Setup(x => x.GetPagedProducts(filter)).Returns(productsList);

            // Act
            var result = _productService.GetPagedProducts(filter);

            // Assert
            result.Should().BeEquivalentTo(expectedProductDtoList);
        }

        [Fact]
        public void Deve_AdicionarProduto_QuandoProdutoValido_RegistraProdutoNoRepositorio()
        {
            // Arrange
            var productDto = ProductFaker.GenerateFakeProductDto().Generate();

            // Act
            _productService.AddProduct(productDto);

            // Assert
            _mocker.GetMock<IProductRepository>().Verify(x => x.Add(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void Deve_AtualizarProduto_QuandoProdutoValido_AtualizaProdutoNoRepositorio()
        {
            // Arrange
            var productDto = ProductFaker.GenerateFakeProductDto().Generate();
            var existingProductEntity = _mapper.Map<Product>(productDto);

            _mocker.GetMock<IProductRepository>()
                .Setup(x => x.GetById(productDto.Id)).Returns(existingProductEntity);

            // Act
            _productService.UpdateProduct(productDto);

            // Assert
            _mocker.GetMock<IProductRepository>().Verify(x => x.Update(existingProductEntity), Times.Once);
        }

        [Fact]
        public void Deve_AtualizarProduto_QuandoProdutoNaoEncontrado_GeraExcecaoNotFoundException()
        {
            // Arrange
            var productDto = ProductFaker.GenerateFakeProductDto().Generate();

            _mocker.GetMock<IProductRepository>().Setup(x => x.GetById(0)).Returns((Product)null);

            // Act & Assert
            Assert.Throws<NotFoundException>(() => _productService.UpdateProduct(productDto));
        }

        [Fact]
        public void Deve_ExcluirProduto_QuandoIdProdutoValido_RealizaExclusaoSoftNoRepositorio()
        {
            // Arrange
            var productId = 1;

            // Act
            _productService.SoftDeleteProduct(productId);

            // Assert
            _mocker.GetMock<IProductRepository>().Verify(x => x.SoftDelete(productId), Times.Once);
        }
    }
}