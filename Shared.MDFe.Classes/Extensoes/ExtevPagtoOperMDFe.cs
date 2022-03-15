using DFe.Utils;
using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFe.Utils.Validacao;

namespace MDFe.Classes.Extencoes
{
    public static class ExtevPagtoOperMDFe
    {
        public static void ValidaSchema(this evPagtoOperMDFe evIncDFeMDFe, VersaoServico versaoLayout, string caminhoSchemas)
        {
            var ev = evIncDFeMDFe.XmlString();

            switch (versaoLayout)
            {
                case VersaoServico.Versao300:
                    Validador.Valida(ev, "evPagtoOperMDFe_v3.00.xsd",caminhoSchemas);
                    break;
            }
        }

        public static string XmlString(this evPagtoOperMDFe evIncDFeMDFe)
        {
            return FuncoesXml.ClasseParaXmlString(evIncDFeMDFe);
        }
    }
}