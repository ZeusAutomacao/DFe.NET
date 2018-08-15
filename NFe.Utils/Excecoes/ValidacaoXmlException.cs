using System;

namespace NFe.Utils.Excecoes
{
    public class ValidacaoXmlException : Exception
    {
        public ValidacaoXmlException(string message) : base("Falha na validação do XML: " + message)
        {
        }
    }
}
