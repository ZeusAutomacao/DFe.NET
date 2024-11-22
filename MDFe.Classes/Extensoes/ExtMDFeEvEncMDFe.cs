using DFe.Utils;
using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFe.Utils.Validacao;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeEvEncMDFe
    {
        public static void ValidaSchema(this MDFeEvEncMDFe evEncMDFe, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var xmlEncerramento = evEncMDFe.XmlString();

            switch (config.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlEncerramento, "evEncMDFe_v1.00.xsd", config);
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlEncerramento, "evEncMDFe_v3.00.xsd", config);
                    break;
            }
        }

        public static string XmlString(this MDFeEvEncMDFe evEncMDFe)
        {
            return FuncoesXml.ClasseParaXmlString(evEncMDFe);
        }
    }
}