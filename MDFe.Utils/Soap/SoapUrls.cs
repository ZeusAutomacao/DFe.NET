using System;
using System.Collections.Generic;
using System.Text;
using static MDFe.Utils.Enums.Enums;

namespace MDFe.Utils.Soap
{
    public class SoapUrls
    {
        public string GetSoapUrl(Tipo buscaTipo)
        {
            switch (buscaTipo)
            {
                case Tipo.MDFeConsNaoEnc:
                    return "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeConsNaoEnc/mdfeConsNaoEnc";
                case Tipo.MDFeConsulta:
                    return "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeConsulta/mdfeConsultaMDF";
                case Tipo.MDFeRecepcaoEvento:
                    return "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeRecepcaoEvento/mdfeRecepcaoEvento";
                case Tipo.MDFeRecepcao:
                    return "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeRecepcao/mdfeRecepcaoLote";
                case Tipo.MDFeRetRecepcao:
                    return "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeRetRecepcao/mdfeRetRecepcao";
                case Tipo.MDFeStatusServico:
                    return "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeStatusServico/mdfeStatusServicoMDF";
                default:
                    throw new ArgumentOutOfRangeException(nameof(buscaTipo), buscaTipo, null);
            }
        }
    }
}
