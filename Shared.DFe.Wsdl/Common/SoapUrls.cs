using System;
using CTe.CTeOSDocumento.Common;

namespace CTe.CTeOSDocumento.Soap
{
    public class SoapUrls
    {
        public string GetSoapUrl(TipoEvento tipoEvento)
        {
            switch (tipoEvento)
            {
                case TipoEvento.CTeConsulta:
                    return "http://www.portalfiscal.inf.br/cte/wsdl/CteConsulta";
                case TipoEvento.CTeDistribuicaoDFe:
                    return "http://www.portalfiscal.inf.br/cte/wsdl/CTeDistribuicaoDFe/cteDistDFeInteresse";
                case TipoEvento.CTeRecepcaoEvento:
                    return "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoEvento";
                case TipoEvento.CTeInutilizacao:
                    return "http://www.portalfiscal.inf.br/cte/wsdl/CteInutilizacao";
                case TipoEvento.CTeRecepcao:
                    return "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcao";
                case TipoEvento.CTeRetRecepcao:
                    return "http://www.portalfiscal.inf.br/cte/wsdl/CteRetRecepcao";
                case TipoEvento.CTeStatusServico:
                    return "http://www.portalfiscal.inf.br/cte/wsdl/CteStatusServico";
                case TipoEvento.MDFeStatusServico:
                    return "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeStatusServico";
                case TipoEvento.MDFeRecepcao:
                    return "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeRecepcao";
                case TipoEvento.MDFeRetRecepcao:
                    return "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeRetRecepcao";
                case TipoEvento.MDFeNaoEncerrado:
                    return "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeConsNaoEnc";
                case TipoEvento.MDFeConsulta:
                    return "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeConsulta";
                case TipoEvento.MDFeEvento:
                    return "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeRecepcaoEvento";
                default:
                    throw new ArgumentOutOfRangeException(nameof(tipoEvento), tipoEvento, null);
            }
        } 
    }
}