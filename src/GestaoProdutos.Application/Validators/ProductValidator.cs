using FluentValidation;
using GestaoProdutos.Domain.Entities;
using System;
using System.Text.RegularExpressions;

namespace GestaoProdutos.Application.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Description).NotEmpty().WithMessage(ProductValidationMessages.DescriptionRequired);
            RuleFor(product => product.Status).IsInEnum().WithMessage(ProductValidationMessages.StatusInvalid);

            // Validação da data de fabricação e validade
            RuleFor(product => product.ManufacturingDate)
                .LessThan(product => product.ExpiryDate)
                .When(product => product.ExpiryDate > DateTime.MinValue) // Verifica se ExpiryDate não é o valor padrão (DateTime.MinValue)
                .WithMessage(ProductValidationMessages.ManufacturingDateMustBeBeforeExpiryDate);

            // Validação do CNPJ (somente se fornecido)
            RuleFor(product => product.SupplierCnpj)
            .Must(cnpj => string.IsNullOrEmpty(cnpj) || Regex.IsMatch(cnpj, @"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$"))
            .WithMessage(ProductValidationMessages.InvalidSupplierCnpjFormat);
        }
    }


    public static class ProductValidationMessages
    {
        public const string DescriptionRequired = "A descrição do produto é obrigatória.";
        public const string StatusInvalid = "O status do produto deve ser Ativo ou Inativo.";
        public const string ManufacturingDateMustBeBeforeExpiryDate = "A data de fabricação deve ser anterior à data de validade.";
        public const string InvalidSupplierCnpjFormat = "Formato inválido para o CNPJ do fornecedor. 12.345.678/0001-00";
    }
}