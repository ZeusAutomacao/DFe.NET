using DFe.Utils;
using NFe.Classes.Servicos.ConsultaGtin;

namespace NFe.Utils.ConsultaGtin
{
    public static class ExtconsGTIN
    {
        public static string ObterXmlString(this consGTIN consGtin)
        {
            return FuncoesXml.ClasseParaXmlString(consGtin);
        }

        public static retConsGTIN CarregarDeXmlString(this retConsGTIN retConsCad, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retConsGTIN>(xmlString);
        }

        public static string ObterXmlString(this retConsGTIN retConsGTIN)
        {
            return FuncoesXml.ClasseParaXmlString(retConsGTIN);
        }
    }
}