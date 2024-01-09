using GestaoProdutos.Domain.Enums;
using System;
using System.ComponentModel;

namespace GestaoProdutos.Domain.Enums
{
    public enum ProductStatus
    {
        [Description("Ativo")]
        Ativo,
        [Description("Inativo")]
        Inativo
    }
}

public static class ProductStatusExtensions
{
    public static string GetDescription(this ProductStatus status)
    {
        var field = status.GetType().GetField(status.ToString());
        var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attribute == null ? status.ToString() : attribute.Description;
    }
}
