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
        public static void ValidaSchema(this MDFeConsReciMDFe consReciMDFe)
        {
            var xmlValidacao = consReciMDFe.XmlString();

            switch (MDFeConfiguracao.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlValidacao, "consReciMDFe_v1.00.xsd");
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlValidacao, "consReciMDFe_v3.00.xsd");
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

        public static void SalvarXmlEmDisco(this MDFeConsReciMDFe consReciMDFe)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, consReciMDFe.NRec + "-ped-rec.xml");

            FuncoesXml.ClasseParaArquivoXml(consReciMDFe, arquivoSalvar);
        }
    }
}