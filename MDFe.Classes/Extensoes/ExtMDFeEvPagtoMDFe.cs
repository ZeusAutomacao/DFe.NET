using DFe.Utils;
using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFe.Utils.Validacao;

namespace MDFe.Classes.Extensoes
{
    public static class ExtMDFeEvPagtoMDFe
    {
        public static void ValidaSchema(this MDFeEvPagtoMDFe evPagtoMDFe, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;

            var xmlIncluirDFe = evPagtoMDFe.XmlString();

            switch (config.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao300:
                    Validador.Valida(xmlIncluirDFe, "evInclusaoDFeMDFe_v3.00.xsd", config);
                    break;
            }
        }

        public static string XmlString(this MDFeEvPagtoMDFe evPagtoMDFe)
        {
            return FuncoesXml.ClasseParaXmlString(evPagtoMDFe);
        }
    }
}
