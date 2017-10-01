using System.Text;
using DFe.DocumentosEletronicos.Entidades;
using DFe.DocumentosEletronicos.Ext;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.Wsdl.Cabecalho
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