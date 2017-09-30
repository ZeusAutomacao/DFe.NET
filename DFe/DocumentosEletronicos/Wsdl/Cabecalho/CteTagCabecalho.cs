using System.Text;
using DFe.DocumentosEletronicos.Entidades;
using DFe.DocumentosEletronicos.Ext;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.Wsdl.Cabecalho
{
    public class CteTagCabecalho : ITagCabecalho
    {
        public string GetTagCabecalho(DFeCabecalho dfeCabecalho)
        {
            StringBuilder env = new StringBuilder();

            env.Append("<cteCabecMsg xmlns=\"");

            env.Append(dfeCabecalho.NamespaceHeader);

            env.Append("\">");

            env.Append("<cUF>");
            env.Append(dfeCabecalho.CodigoUF.GetCodigoIbgeEmString());
            env.Append("</cUF>");

            env.Append("<versaoDados>");
            env.Append(dfeCabecalho.VersaoServico.GetVersaoString());
            env.Append("</versaoDados>");

            env.Append("</cteCabecMsg>");

            return env.ToString();
        }
    }
}