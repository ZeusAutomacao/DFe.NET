using System.IO;
using System.Xml;
using DFe.Utils;
using MDFe.Classes.Informacoes.StatusServico;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFe.Utils.Validacao;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeConsStatServMDFe
    {
        public static void ValidarSchema(this MDFeConsStatServMDFe consStatServMDFe)
        {
            var xmlValidacao = consStatServMDFe.XmlString();

            switch (MDFeConfiguracao.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlValidacao, "consStatServMDFe_v1.00.xsd");
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlValidacao, "consStatServMDFe_v3.00.xsd");
                    break;
            }
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

            var arquivoSalvar = Path.Combine(caminhoXml, "-pedido-status-servico.xml");

            FuncoesXml.ClasseParaArquivoXml(consStatServMdFe, arquivoSalvar);
        }
    }
}