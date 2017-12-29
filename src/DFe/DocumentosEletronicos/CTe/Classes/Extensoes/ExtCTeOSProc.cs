using DFe.DocumentosEletronicos.CTe.CTeOS.Retorno;
using DFe.DocumentosEletronicos.ManipuladorDeXml;

namespace DFe.DocumentosEletronicos.CTe.Classes.Extensoes
{
    public static class ExtCTeOSProc
    {
        public static string ObterXmlString(this cteOSProc cteOsProc)
        {
            return FuncoesXml.ClasseParaXmlString(cteOsProc);
        }
    }
}