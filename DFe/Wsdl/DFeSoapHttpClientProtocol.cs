using DFe.DocumentosEletronicos.Wsdl;
using DFe.Http;

namespace DFe.Wsdl
{
    public abstract class DFeSoapHttpClientProtocol
    {
        protected string Invoke(DFeSoapConfig soapConfig)
        {
            var request = new RequestWS().EnviaSefaz(soapConfig);

            return request;
        }
    }
}