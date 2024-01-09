using Bogus;
using Bogus.Extensions.Brazil;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Enums;
using GestaoProdutos.Infrastructure.EF.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GestaoProdutos.Infrastructure.EF
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMap());

            // Adicionar produtos fictícios à base de dados em memória
            var fakeProducts = GenerateFakeProducts(30); // Gera 10 produtos fake
            modelBuilder.Entity<Product>().HasData(fakeProducts);

            base.OnModelCreating(modelBuilder);
        }

        private static List<Product> GenerateFakeProducts(int count)
        {
            var productFaker = new Faker<Product>("pt_BR")
                .RuleFor(p => p.Id, f => f.IndexFaker + 1)
                .RuleFor(p => p.Description, f => f.Commerce.ProductName())
                .RuleFor(p => p.Status, f => f.PickRandom<ProductStatus>())
                .RuleFor(p => p.ManufacturingDate, f => f.Date.Past())
                .RuleFor(p => p.ExpiryDate, (f, p) => f.Date.Future(1, p.ManufacturingDate))
                .RuleFor(p => p.SupplierCode, f => f.Random.Number(100, 999))
                .RuleFor(p => p.SupplierDescription, f => f.Company.CompanyName())
                .RuleFor(p => p.SupplierCnpj, f => f.Company.Cnpj());

            return productFaker.Generate(count);
        }
    }
}
