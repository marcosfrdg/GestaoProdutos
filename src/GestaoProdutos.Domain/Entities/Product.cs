using GestaoProdutos.Domain.Enums;
using System;

namespace GestaoProdutos.Domain.Entities
{
    public class Product : BaseEntity<int>
    {
        public string Description { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int? SupplierCode { get; set; }
        public string SupplierDescription { get; set; }
        public string SupplierCnpj { get; set; }
    }
}
