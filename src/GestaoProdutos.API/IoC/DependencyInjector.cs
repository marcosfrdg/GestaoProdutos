using FluentValidation;
using GestaoProdutos.Application.AutoMapper;
using GestaoProdutos.Application.Produto;
using GestaoProdutos.Application.Validators;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Interfaces.Repositories;
using GestaoProdutos.Infrastructure.EF;
using GestaoProdutos.Infrastructure.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoProdutos.API.IoC
{
    public static class DependencyInjector
    {
        public static void AddInfraRepositories(this IServiceCollection services)
        {
            //Injetando a string de conexão no DbContext
            services.AddDbContext<DataContext>(options =>
                options.UseInMemoryDatabase("InMemoryDatabase"));

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddAutoMapper(typeof(AutoMapperConfig));

            services.AddTransient<IValidator<Product>, ProductValidator>();

            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            dbContext.Database.EnsureCreated(); // Garante que o banco de dados em memória está criado
        }
    }
}
