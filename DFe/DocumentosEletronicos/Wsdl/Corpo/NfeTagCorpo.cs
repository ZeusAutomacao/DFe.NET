using System.Text;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.Wsdl.Corpo
{
    public class NfeTagCorpo : ITagCorpo
    {
        public string GetTagCorpo(DFeCorpo dfeCorpo)
        {
            StringBuilder env = new StringBuilder();

            env.Append("<nfeDadosMsg xmlns=\"");
            env.Append(dfeCorpo.NamespaceBody);
            env.Append("\">");
            env.Append(dfeCorpo.GetXml());
            env.Append("</nfeDadosMsg>");

            return env.ToString();
        }
    }
}