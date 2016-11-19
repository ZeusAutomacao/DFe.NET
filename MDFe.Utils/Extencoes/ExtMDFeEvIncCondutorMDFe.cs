using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Utils.Validacao;

namespace MDFe.Utils.Extencoes
{
    public static class ExtMDFeEvIncCondutorMDFe
    {
        public static void ValidaSchema(this MDFeEvIncCondutorMDFe evIncCondutorMDFe)
        {
            var xmlIncluirCondutor = evIncCondutorMDFe.XmlString();

            Validador.Valida(xmlIncluirCondutor, "evIncCondutorMDFe_v1.00.xsd");
        }

        public static string XmlString(this MDFeEvIncCondutorMDFe evIncCondutorMDFe)
        {
            return FuncoesXml.ClasseParaXmlString(evIncCondutorMDFe);
        }
    }
}