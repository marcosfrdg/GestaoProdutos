using NetArchTest.Rules;
using Xunit;

namespace GestaoProdutos.Arhitecture.Tests
{
    public class ArhitectureTests
    {
        private const string DomainNamespace = "GestaoProdutos.Domain";
        private const string ApplicationNamespace = "GestaoProdutos.Application";
        private const string InfrastructureNamespace = "GestaoProdutos.Infrastructure";
        private const string PresentationNamespace = "GestaoProdutos.Presentation";
        private const string WebApiNamespace = "GestaoProdutos.API";

        [Fact]
        public void Domain_NaoDeveTerDependenciaComOutrosProjetos()
        {
            // Arrange
            var assembly = typeof(Domain.AssemblyReference).Assembly;

            var outrosProjetos = new[]
            {
                ApplicationNamespace,
                InfrastructureNamespace,
                PresentationNamespace,
                WebApiNamespace
            };

            // Act
            var resultadoDoTeste = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(outrosProjetos)
                .GetResult();

            // Assert
            Assert.True(resultadoDoTeste.IsSuccessful);
        }

        [Fact]
        public void Application_NaoDeveTerDependenciaComOutrosProjetos()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;

            var outrosProjetos = new[]
            {
                InfrastructureNamespace,
                PresentationNamespace,
                WebApiNamespace
            };

            // Act
            var resultadoDoTeste = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(outrosProjetos)
                .GetResult();

            // Assert
            Assert.True(resultadoDoTeste.IsSuccessful);
        }

        [Fact]
        public void Handlers_DevemTerDependenciaComDomain()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;

            // Act
            var resultadoDoTeste = Types
                .InAssembly(assembly)
                .That()
                .HaveNameEndingWith("Handler")
                .Should()
                .HaveDependencyOn(DomainNamespace)
                .GetResult();

            // Assert
            Assert.True(resultadoDoTeste.IsSuccessful);
        }

        [Fact]
        public void Infrastructure_NaoDeveTerDependenciaComOutrosProjetos()
        {
            // Arrange
            var assembly = typeof(Infrastructure.AssemblyReference).Assembly;

            var outrosProjetos = new[]
            {
                PresentationNamespace,
                WebApiNamespace
            };

            // Act
            var resultadoDoTeste = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(outrosProjetos)
                .GetResult();

            // Assert
            Assert.True(resultadoDoTeste.IsSuccessful);
        }

        [Fact]
        public void Presentation_NaoDeveTerDependenciaComOutrosProjetos()
        {
            // Arrange
            var assembly = typeof(Infrastructure.AssemblyReference).Assembly;

            var outrosProjetos = new[]
            {
                InfrastructureNamespace,
                WebApiNamespace
            };

            // Act
            var resultadoDoTeste = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(outrosProjetos)
                .GetResult();

            // Assert
            Assert.True(resultadoDoTeste.IsSuccessful);
        }

    }
}