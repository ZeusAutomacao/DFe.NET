using DFe.DocumentosEletronicos.CTe.CTeOS.Servicos.Autorizacao;
using DFe.DocumentosEletronicos.Wsdl;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.CTe.Wsdl.Gerado.CTeRecepcao
{
    public class CteRecepcaoOSS : DFeSoapHttpClientProtocol
    {
        public retCTeOS Autorizar(DFeSoapConfig soapConfig)
        {
            var ret = Invoke(soapConfig);

            var retCteOs = retCTeOS.LoadXml(ret);

            return retCteOs;
        }
    }
}