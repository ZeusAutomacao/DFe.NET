using System.Xml;
using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.StatusServico;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Validacao;

namespace MDFe.Utils.Extencoes
{
    public static class ExtMDFeConsStatServMDFe
    {
        public static void ValidarSchema(this MDFeConsStatServMDFe consStatServMDFe)
        {
            var xmlValidacao = consStatServMDFe.XmlString();

            Validador.Valida(xmlValidacao, "consStatServMDFe_v1.00.xsd");
        }

        public static string XmlString(this MDFeConsStatServMDFe consStatServMDFe)
        {
            return FuncoesXml.ClasseParaXmlString(consStatServMDFe);
        }

        public static XmlDocument CriaRequestWs(this MDFeConsStatServMDFe consStatServMdFe)
        {
            var request = new XmlDocument();
            request.LoadXml(consStatServMdFe.XmlString());

            return request;
        }

        public static void SalvarXmlEmDisco(this MDFeConsStatServMDFe consStatServMdFe)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\-pedido-status-servico.xml";

            FuncoesXml.ClasseParaArquivoXml(consStatServMdFe, arquivoSalvar);
        }
    }
}