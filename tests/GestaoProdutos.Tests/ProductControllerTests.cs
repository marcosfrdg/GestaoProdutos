using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using GestaoProdutos.API.Controllers;
using GestaoProdutos.Application.AutoMapper;
using GestaoProdutos.Application.Produto;
using GestaoProdutos.Application.Validators;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Enums;
using GestaoProdutos.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using Xunit;

namespace GestaoProdutos.Tests
{
    public class ProductControllerTests
    {
        private readonly ProductController _controller;
        private readonly ProductValidator _validator;
        private readonly AutoMocker _mocker;
        private readonly IMapper _mapper;

        public ProductControllerTests()
        {
            _mocker = new AutoMocker();

            var myProfile = _mocker.CreateInstance<AutoMapperConfig>();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            _mapper = new Mapper(configuration);
            _mocker.Use(_mapper);

            _controller = _mocker.CreateInstance<ProductController>();
            _validator = _mocker.CreateInstance<ProductValidator>();
        }

        [Fact]
        public void GetProductById_ExistingId_ReturnsOkResult()
        {
            // Arrange
            _mocker.GetMock<IProductService>()
                   .Setup(x => x.GetProductById(It.IsAny<int>())).Returns(new ProductDto { Id = 1 });

            // Act
            var result = _controller.GetProductById(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetProductById_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            _mocker.GetMock<IProductService>()
                .Setup(x => x.GetProductById(It.IsAny<int>())).Returns((ProductDto)null);

            // Act
            var result = _controller.GetProductById(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetPagedProducts_ReturnsOkResult()
        {
            // Arrange
            _mocker.GetMock<IProductService>()
                .Setup(x => x.GetPagedProducts(It.IsAny<string>())).Returns(new List<ProductDto>());

            // Act
            var result = _controller.GetPagedProducts();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void AddProduct_ValidProduct_ReturnsCreatedAtActionResult()
        {
            // Arrange
            _mocker.GetMock<IValidator<Product>>()
                .Setup(x => x.Validate(It.IsAny<Product>()))
                .Returns(new ValidationResult());

            var productDto = new ProductDto
            {
                Id = 1,
                Description = "Teste Produto",
                Status = ProductStatus.Ativo,
                StatusDescription = "Ativo",
                ManufacturingDate = DateTime.Now.AddMonths(-3),
                ExpiryDate = DateTime.Now.AddMonths(6),
                SupplierCode = 123,
                SupplierDescription = "Teste fornecedor",
                SupplierCnpj = "12345678901234"
            };

            // Act
            var result = _controller.AddProduct(productDto);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public void AddProduct_InvalidProduct_ReturnsErrorValidation()
        {
            // Arrange
            var productDto = new ProductDto
            {
                Id = 1,
                Description = "",
                Status = ProductStatus.Ativo,
                StatusDescription = "Ativo",
                ManufacturingDate = DateTime.Now.AddMonths(-3),
                ExpiryDate = DateTime.Now.AddMonths(6),
                SupplierCode = 123,
                SupplierDescription = "Teste fornecedor",
                SupplierCnpj = "12.345.678/0001-34"
            };

            _mocker.GetMock<IValidator<Product>>()
                .Setup(x => x.Validate(It.IsAny<Product>()))
                .Returns(_validator.Validate(_mapper.Map<Product>(productDto)));

            // Act
            var result = _controller.AddProduct(productDto);
            var retorno = result.Should().BeOfType<BadRequestObjectResult>().Which;

            // Assert
            retorno.StatusCode.Should().Be(400);

            retorno.Value.Should().BeEquivalentTo(
                new { Errors = new[] { "A descrição do produto é obrigatória." } },
                options => options.ExcludingMissingMembers());
        }


        [Fact]
        public void UpdateProduct_ValidProduct_ReturnsNoContentResult()
        {
            // Arrange
            _mocker.GetMock<IValidator<Product>>()
                .Setup(x => x.Validate(It.IsAny<Product>()))
                .Returns(new ValidationResult());

            var productDto = new ProductDto
            {
                Id = 1,
                Description = "Updated Produto",
                Status = ProductStatus.Inativo,
                StatusDescription = "Out of stock",
                ManufacturingDate = DateTime.Now.AddMonths(-2),
                ExpiryDate = DateTime.Now.AddMonths(8),
                SupplierCode = 456,
                SupplierDescription = "Updated Fornecedor",
                SupplierCnpj = "98765432101234"
            };

            // Act
            var result = _controller.UpdateProduct(1, productDto);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void UpdateProduct_InvaliIdProduct_ReturnsBadRequest()
        {
            // Arrange
            _mocker.GetMock<IValidator<Product>>()
                .Setup(x => x.Validate(It.IsAny<Product>()))
                .Returns(new ValidationResult());

            var productDto = new ProductDto
            {
                Id = 1,
                Description = "Updated Produto",
                Status = ProductStatus.Inativo,
                StatusDescription = "Out of stock",
                ManufacturingDate = DateTime.Now.AddMonths(-2),
                ExpiryDate = DateTime.Now.AddMonths(8),
                SupplierCode = 456,
                SupplierDescription = "Updated Fornecedor",
                SupplierCnpj = "98765432101234"
            };

            // Act
            var result = _controller.UpdateProduct(2, productDto) as ObjectResult;

            // Assert
            result.StatusCode.Should().Be(400);
            result.Value.Should().Be("ID do produto informado é inválido.");
        }

        [Fact]
        public void UpdateProduct_InvalidProductDescription_ReturnsBadRequest()
        {
            // Arrange
            var productDto = new ProductDto
            {
                Id = 1,
                Description = "",
                Status = ProductStatus.Inativo,
                StatusDescription = "",
                ManufacturingDate = DateTime.Now.AddMonths(-2),
                ExpiryDate = DateTime.Now.AddMonths(8),
                SupplierCode = 456,
                SupplierDescription = "Updated Fornecedor",
                SupplierCnpj = "12.345.678/0001-34"
            };

            _mocker.GetMock<IValidator<Product>>()
               .Setup(x => x.Validate(It.IsAny<Product>()))
               .Returns(_validator.Validate(_mapper.Map<Product>(productDto)));

            // Act
            var result = _controller.UpdateProduct(1, productDto);
            var retorno = result.Should().BeOfType<BadRequestObjectResult>().Which;

            // Assert
            retorno.StatusCode.Should().Be(400);

            retorno.Value.Should().BeEquivalentTo(
                new { Errors = new[] { "A descrição do produto é obrigatória." } },
                options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void DeleteProduct_ExistingId_ReturnsNoContentResult()
        {
            // Arrange
            _mocker.GetMock<IProductService>()
                .Setup(x => x.SoftDeleteProduct(It.IsAny<int>()));

            // Act
            var result = _controller.DeleteProduct(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void DeleteProduct_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            _mocker.GetMock<IProductService>()
                .Setup(x => x.SoftDeleteProduct(It.IsAny<int>()))
                .Throws(new NotFoundException("Produto não encontrado"));

            // Act
            var result = _controller.DeleteProduct(1);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public void DeleteProduct_ExceptionThrown_ReturnsInternalServerErrorResult()
        {
            // Arrange
            _mocker.GetMock<IProductService>()
                .Setup(x => x.SoftDeleteProduct(It.IsAny<int>()))
                .Throws(new Exception("Some error"));

            // Act
            var result = _controller.DeleteProduct(1);

            // Assert
            result.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(500);
        }
    }
}