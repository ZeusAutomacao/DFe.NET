using DFe.DocumentosEletronicos.Wsdl;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.CTe.Wsdl.Gerado.CTeRecepcao
{
    public class CteRecepcaoOSS : DFeSoapHttpClientProtocol
    {
        public string Autorizar(DFeSoapConfig soapConfig)
        {
            var ret = Invoke(soapConfig);

            return ret;
        }
    }
}