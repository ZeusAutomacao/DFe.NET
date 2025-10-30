using DFe.Utils;
using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFe.Utils.Validacao;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeEvIncDFeMDFe
    {
        public static void ValidaSchema(this MDFeEvIncDFeMDFe evIncDFeMDFe, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var xmlIncluirDFe = evIncDFeMDFe.XmlString();

            switch (config.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao300:
                    Validador.Valida(xmlIncluirDFe, "evInclusaoDFeMDFe_v3.00.xsd", config);
                    break;
            }
        }

        public static string XmlString(this MDFeEvIncDFeMDFe evIncDFeMDFe)
        {
            return FuncoesXml.ClasseParaXmlString(evIncDFeMDFe);
        }
    }
}