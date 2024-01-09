using Bogus;
using Bogus.Extensions.Brazil;
using GestaoProdutos.Application.Produto;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Enums;

namespace GestaoProdutos.Tests
{
    public static class ProductFaker
    {
        public static Faker<Product> GenerateFakeProduct()
        {
            return new Faker<Product>("pt_BR")
                .RuleFor(p => p.Id, f => f.IndexFaker + 1)
                .RuleFor(p => p.Description, f => f.Commerce.ProductName())
                .RuleFor(p => p.Status, f => f.PickRandom<ProductStatus>())
                .RuleFor(p => p.ManufacturingDate, f => f.Date.Past())
                .RuleFor(p => p.ExpiryDate, (f, p) => f.Date.Future(1, p.ManufacturingDate))
                .RuleFor(p => p.SupplierCode, f => f.Random.Number(100, 999))
                .RuleFor(p => p.SupplierDescription, f => f.Company.CompanyName())
                .RuleFor(p => p.SupplierCnpj, f => f.Company.Cnpj());
        }

        public static Faker<ProductDto> GenerateFakeProductDto()
        {
            return new Faker<ProductDto>("pt_BR")
                .RuleFor(p => p.Id, f => f.IndexFaker + 1)
                .RuleFor(p => p.Description, f => f.Commerce.ProductName())
                .RuleFor(p => p.Status, f => f.PickRandom<ProductStatus>())
                .RuleFor(p => p.StatusDescription, f => f.PickRandom<ProductStatus>().GetDescription())
                .RuleFor(p => p.ManufacturingDate, f => f.Date.Past())
                .RuleFor(p => p.ExpiryDate, (f, p) => f.Date.Future(1, p.ManufacturingDate))
                .RuleFor(p => p.SupplierCode, f => f.Random.Number(100, 999))
                .RuleFor(p => p.SupplierDescription, f => f.Company.CompanyName())
                .RuleFor(p => p.SupplierCnpj, f => f.Company.Cnpj());
        }
    }
}
