using System.Text;
using DFe.Classes.Extensoes;
using DFe.Wsdl;

namespace CTe.CTeOSDocumento.Wsdl.Cabecalho
{
    public class NfeTagCabecalho : ITagCabecalho
    {
        public string GetTagCabecalho(DFeCabecalho dfeCabecalho)
        {
            StringBuilder env = new StringBuilder();

            env.Append("<nfeCabecMsg xmlns=\"");

            env.Append(dfeCabecalho.NamespaceHeader);

            env.Append("\">");

            env.Append("<cUF>");
            env.Append(dfeCabecalho.CodigoUF.GetCodigoIbgeEmString());
            env.Append("</cUF>");

            env.Append("<versaoDados>");
            env.Append(dfeCabecalho.VersaoServico.GetVersaoString());
            env.Append("</versaoDados>");

            env.Append("</nfeCabecMsg>");

            return env.ToString();
        }
    }
}