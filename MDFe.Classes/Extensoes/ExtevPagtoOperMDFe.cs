using DFe.Utils;
using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFe.Utils.Validacao;

namespace MDFe.Classes.Extencoes
{
    public static class ExtevPagtoOperMDFe
    {
        public static void ValidaSchema(this evPagtoOperMDFe evIncDFeMDFe)
        {
            var ev = evIncDFeMDFe.XmlString();

            switch (MDFeConfiguracao.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao300:
                    Validador.Valida(ev, "evPagtoOperMDFe_v3.00.xsd");
                    break;
            }
        }

        public static string XmlString(this evPagtoOperMDFe evIncDFeMDFe)
        {
            return FuncoesXml.ClasseParaXmlString(evIncDFeMDFe);
        }
    }
}