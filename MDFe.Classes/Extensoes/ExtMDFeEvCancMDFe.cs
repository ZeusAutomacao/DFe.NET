using DFe.Utils;
using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFe.Utils.Validacao;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeEvCancMDFe
    {
        public static void ValidaSchema(this MDFeEvCancMDFe evCancMDFe, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var xmlCancelamento = evCancMDFe.XmlString();

            switch (config.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlCancelamento, "evCancMDFe_v1.00.xsd", config);
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlCancelamento, "evCancMDFe_v3.00.xsd", config);
                    break;
            }
        }

        public static string XmlString(this MDFeEvCancMDFe evCancMDFe)
        {
            return FuncoesXml.ClasseParaXmlString(evCancMDFe);
        }
    }
}