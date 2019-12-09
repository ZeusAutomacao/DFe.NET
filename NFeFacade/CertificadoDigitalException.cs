using System;

namespace NFeFacade
{
    public class CertificadoDigitalException : InvalidOperationException
    {
        public CertificadoDigitalException(string message) : base(message)
        {
        }
    }
}