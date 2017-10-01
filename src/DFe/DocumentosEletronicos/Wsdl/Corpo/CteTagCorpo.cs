using System.Text;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.Wsdl.Corpo
{
    public class CteTagCorpo : ITagCorpo
    {
        public string GetTagCorpo(DFeCorpo dfeCorpo)
        {
            StringBuilder env = new StringBuilder();

            env.Append("<cteDadosMsg xmlns=\"");
            env.Append(dfeCorpo.NamespaceBody);
            env.Append("\">");
            env.Append(dfeCorpo.GetXml());
            env.Append("</cteDadosMsg>");

            return env.ToString();
        }
    }
}