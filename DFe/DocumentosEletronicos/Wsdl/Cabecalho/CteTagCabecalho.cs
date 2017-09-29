using System.Text;
using DFe.DocumentosEletronicos.Entidades;
using DFe.DocumentosEletronicos.Ext;

namespace DFe.DocumentosEletronicos.Wsdl.Cabecalho
{
    public class CteTagCabecalho : ITagCabecalho
    {
        public string GetTagCabecalho(DFeSoapConfig soapConfig)
        {
            StringBuilder env = new StringBuilder();

            env.Append("<cteCabecMsg xmlns=\"");

            env.Append(soapConfig.NamespaceHeader);

            env.Append("\">");

            env.Append("<cUF>");
            env.Append(soapConfig.DFeCabecalho.CodigoUF.GetCodigoIbgeEmString());
            env.Append("</cUF>");

            env.Append("<versaoDados>");
            env.Append(soapConfig.DFeCabecalho.VersaoServico.GetVersaoString());
            env.Append("</versaoDados>");

            env.Append("</cteCabecMsg>");

            return env.ToString();
        }
    }
}