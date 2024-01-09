using System;

namespace GestaoProdutos.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Registro não encontrado.")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
