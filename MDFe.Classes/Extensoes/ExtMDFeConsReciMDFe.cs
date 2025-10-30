using DFe.Utils;
using MDFe.Classes.Informacoes.RetRecepcao;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFe.Utils.Validacao;
using System.IO;
using System.Xml;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeConsReciMDFe
    {
        public static void ValidaSchema(this MDFeConsReciMDFe consReciMDFe, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var xmlValidacao = consReciMDFe.XmlString();

            switch (config.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlValidacao, "consReciMDFe_v1.00.xsd", config);
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlValidacao, "consReciMDFe_v3.00.xsd", config);
                    break;
            }
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

        public static void SalvarXmlEmDisco(this MDFeConsReciMDFe consReciMDFe, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            if (config.NaoSalvarXml()) return;

            var caminhoXml = config.CaminhoSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, consReciMDFe.NRec + "-ped-rec.xml");

            FuncoesXml.ClasseParaArquivoXml(consReciMDFe, arquivoSalvar);
        }
    }
}