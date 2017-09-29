using DFe.DocumentosEletronicos.Wsdl;

namespace DFe.Wsdl
{
    public abstract class DFeSoapHttpClientProtocol
    {
        protected string Invoke(DFeEnvelope envelope)
        {
            var atributos = GetType().CustomAttributes;


            return string.Empty;
        }
    }
}