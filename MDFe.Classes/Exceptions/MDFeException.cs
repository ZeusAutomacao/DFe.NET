using System;

namespace ManifestoDocumentoFiscalEletronico.Classes.Exceptions
{
    public class MDFeException : Exception
    {
        public MDFeException(string message) : base(message)
        {
        }
    }
}