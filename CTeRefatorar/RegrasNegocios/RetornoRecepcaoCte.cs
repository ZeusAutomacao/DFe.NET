using System.Security.Cryptography.X509Certificates;
using System.Xml;
using FusionCore.DFe.RegrasNegocios.contratos;
using FusionCore.DFe.WsdlCte.Homologacao.Helper;
using FusionCore.DFe.WsdlCte.Homologacao.RetornoRecepcao;
using FusionCore.DFe.XmlCte;

namespace FusionCore.DFe.RegrasNegocios
{
    public class RetornoRecepcaoCte : EnviaSefaz
    {
        public XmlNode Executa(XmlNode xmlEnvio,
            FusionEstadoUFCTe estado,
            X509Certificate2 certificado,
            FusionTipoAmbienteCTe ambiente)
        {
            var url = UrlHelper.ObterUrl(estado, ambiente);

            var cteRetRecepcao = new CteRetRecepcao(url.CteRetRecepcao)
            {
                cteCabecMsgValue = new cteCabecMsg
                {
                    versaoDados = "2.00",
                    cUF = estado.GetCodigoUF()
                }
            };

            cteRetRecepcao.ClientCertificates.Add(certificado);

            var xml = cteRetRecepcao.cteRetRecepcao(xmlEnvio);

            return xml;
        }
    }
}