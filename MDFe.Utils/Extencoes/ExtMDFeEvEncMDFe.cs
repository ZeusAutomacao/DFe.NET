using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Utils.Validacao;

namespace MDFe.Utils.Extencoes
{
    public static class ExtMDFeEvEncMDFe
    {
        public static void ValidaSchema(this MDFeEvEncMDFe evEncMDFe)
        {
            var xmlEncerramento = evEncMDFe.XmlString();

            Validador.Valida(xmlEncerramento, "evEncMDFe_v1.00.xsd");
        }

        public static string XmlString(this MDFeEvEncMDFe evEncMDFe)
        {
            return FuncoesXml.ClasseParaXmlString(evEncMDFe);
        }
    }
}