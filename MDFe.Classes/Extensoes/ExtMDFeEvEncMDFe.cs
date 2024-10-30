using DFe.Utils;
using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFe.Utils.Validacao;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeEvEncMDFe
    {
        public static void ValidaSchema(this MDFeEvEncMDFe evEncMDFe)
        {
            var xmlEncerramento = evEncMDFe.XmlString();

            switch (MDFeConfiguracao.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlEncerramento, "evEncMDFe_v1.00.xsd");
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlEncerramento, "evEncMDFe_v3.00.xsd");
                    break;
            }
        }

        public static string XmlString(this MDFeEvEncMDFe evEncMDFe)
        {
            return FuncoesXml.ClasseParaXmlString(evEncMDFe);
        }
    }
}