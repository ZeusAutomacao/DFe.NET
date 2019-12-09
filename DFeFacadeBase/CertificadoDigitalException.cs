using System;

namespace DFeFacadeBase
{
    public class CertificadoDigitalException : InvalidOperationException
    {
        public CertificadoDigitalException(string message) : base(message)
        {
        }
    }
}