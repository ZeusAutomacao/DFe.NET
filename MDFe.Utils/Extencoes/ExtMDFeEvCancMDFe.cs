using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Utils.Validacao;

namespace MDFe.Utils.Extencoes
{
    public static class ExtMDFeEvCancMDFe
    {
        public static void ValidaSchema(this MDFeEvCancMDFe evCancMDFe)
        {
            var xmlCancelamento = evCancMDFe.XmlString();

            Validador.Valida(xmlCancelamento, "evCancMDFe_v1.00.xsd");
        }

        public static string XmlString(this MDFeEvCancMDFe evCancMDFe)
        {
            return FuncoesXml.ClasseParaXmlString(evCancMDFe);
        }
    }
}