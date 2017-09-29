using DFe.DocumentosEletronicos.Wsdl;
using DFe.Http.SoapAtributos;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.CTe.Wsdl.Gerado.CTeRecepcao
{
    public class CteRecepcaoOSS : DFeSoapHttpClientProtocol
    {

        [DFeSoapHeader("cteCabecMsg", Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoOS/cteRecepcaoOS")]
        [DFeSoapAction(Acao = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoOS/cteRecepcaoOS")]
        public string Autorizar([DFeSoapAtributo("http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoOS/cteRecepcaoOS")] DFeEnvelope envelope)
        {
            var ret = Invoke(envelope);

            return ret;
        }
    }
}