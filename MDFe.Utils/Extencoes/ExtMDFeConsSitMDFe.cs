using System.Xml;
using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.ConsultaProtocolo;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Validacao;

namespace MDFe.Utils.Extencoes
{
    public static class ExtMDFeConsSitMDFe
    {
        public static void ValidarSchema(this MDFeConsSitMDFe consSitMdfe)
        {
            var xmlEnvio = consSitMdfe.XmlString();

            Validador.Valida(xmlEnvio, "consSitMdfe_v1.00.xsd");
        }

        public static string XmlString(this MDFeConsSitMDFe consSitMdfe)
        {
            return FuncoesXml.ClasseParaXmlString(consSitMdfe);
        }

        public static XmlDocument CriaRequestWs(this MDFeConsSitMDFe consSitMdfe)
        {
            var request = new XmlDocument();
            request.LoadXml(consSitMdfe.XmlString());

            return request;
        }

        public static void SalvarXmlEmDisco(this MDFeConsSitMDFe consSitMdfe)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + consSitMdfe.ChMDFe + "-ped-sit.xml";

            FuncoesXml.ClasseParaArquivoXml(consSitMdfe, arquivoSalvar);
        }
    }
}