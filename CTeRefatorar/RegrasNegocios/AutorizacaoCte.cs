using System.Security.Cryptography.X509Certificates;
using System.Xml;
using FusionCore.DFe.RegrasNegocios.contratos;
using FusionCore.DFe.WsdlCte.Homologacao.Helper;
using FusionCore.DFe.WsdlCte.Homologacao.Recepcao;
using FusionCore.DFe.XmlCte;

namespace FusionCore.DFe.RegrasNegocios
{
    public class AutorizacaoCte : EnviaSefaz
    {
        public XmlNode Executa(XmlNode xmlEnvio,
            FusionEstadoUFCTe estado,
            X509Certificate2 certificado,
            FusionTipoAmbienteCTe ambiente)
        {
            var url = UrlHelper.ObterUrl(estado, ambiente);

            var cteRecepcao = new CteRecepcao(url.CteRecepcao)
            {
                cteCabecMsgValue = new cteCabecMsg
                {
                    versaoDados = "2.00",
                    cUF = estado.GetCodigoUF()
                }
            };

            cteRecepcao.ClientCertificates.Add(certificado);

            var xml = cteRecepcao.cteRecepcaoLote(xmlEnvio);

            return xml;
        }
    }
}