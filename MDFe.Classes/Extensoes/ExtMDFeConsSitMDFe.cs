using DFe.Utils;
using MDFe.Classes.Informacoes.ConsultaProtocolo;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFe.Utils.Validacao;
using System.IO;
using System.Xml;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeConsSitMDFe
    {
        public static void ValidarSchema(this MDFeConsSitMDFe consSitMdfe)
        {
            var xmlEnvio = consSitMdfe.XmlString();

            switch (MDFeConfiguracao.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlEnvio, "consSitMDFe_v1.00.xsd");
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlEnvio, "consSitMDFe_v3.00.xsd");
                    break;
            }
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

            var arquivoSalvar = Path.Combine(caminhoXml, consSitMdfe.ChMDFe + "-ped-sit.xml");

            FuncoesXml.ClasseParaArquivoXml(consSitMdfe, arquivoSalvar);
        }
    }
}