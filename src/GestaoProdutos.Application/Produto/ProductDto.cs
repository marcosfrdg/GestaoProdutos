using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GestaoProdutos.Application.Produto
{
    public class ProductDto : BaseEntity<int>
    {
        [Required(ErrorMessage = "A descrição do produto é obrigatória.")]
        public string Description { get; set; }

        [EnumDataType(typeof(ProductStatus), ErrorMessage = "Valor do status inválido.")]
        public ProductStatus Status { get; set; }
        public string StatusDescription { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int? SupplierCode { get; set; }
        public string SupplierDescription { get; set; }
        public string SupplierCnpj { get; set; }
    }
}
