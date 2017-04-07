using System.Security.Cryptography.X509Certificates;
using System.Xml;
using FusionCore.DFe.RegrasNegocios.contratos;
using FusionCore.DFe.WsdlCte.Homologacao.Helper;
using FusionCore.DFe.WsdlCte.Homologacao.Inutilizacao;
using FusionCore.DFe.XmlCte;

namespace FusionCore.DFe.RegrasNegocios
{
    public class InutilizacaoCte : EnviaSefaz
    {
        public XmlNode Executa(XmlNode xmlEnvio,
            FusionEstadoUFCTe estado,
            X509Certificate2 certificado,
            FusionTipoAmbienteCTe ambiente)
        {
            var url = UrlHelper.ObterUrl(estado, ambiente);

            var wsdlInutilizacao = new CteInutilizacao(url.CteInutilizacao)
            {
                cteCabecMsgValue = new cteCabecMsg
                {
                    versaoDados = "2.00",
                    cUF = estado.GetCodigoUF()
                }
            };

            wsdlInutilizacao.ClientCertificates.Add(certificado);

            var xml = wsdlInutilizacao.cteInutilizacaoCT(xmlEnvio);

            return xml;
        }
    }
}