using System;
using DFe.DocumentosEletronicos.Common;

namespace DFe.DocumentosEletronicos.Soap
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(tipoEvento), tipoEvento, null);
            }
        } 
    }
}