using System.Xml;
using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.RetRecepcao;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Validacao;

namespace MDFe.Utils.Extencoes
{
    public static class ExtMDFeConsReciMDFe
    {
        public static void ValidaSchema(this MDFeConsReciMDFe consReciMDFe)
        {
            var xmlValidacao = consReciMDFe.XmlString();

            Validador.Valida(xmlValidacao, "consReciMdfe_v1.00.xsd");
        }

        public static string XmlString(this MDFeConsReciMDFe consReciMDFe)
        {
            return FuncoesXml.ClasseParaXmlString(consReciMDFe);
        }

        public static XmlDocument CriaRequestWs(this MDFeConsReciMDFe consReciMDFe)
        {
            var request = new XmlDocument();
            request.LoadXml(consReciMDFe.XmlString());

            return request;
        }

        public static void SalvarXmlEmDisco(this MDFeConsReciMDFe consReciMDFe)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + consReciMDFe.NRec + "-ped-rec.xml";

            FuncoesXml.ClasseParaArquivoXml(consReciMDFe, arquivoSalvar);
        }
    }
}