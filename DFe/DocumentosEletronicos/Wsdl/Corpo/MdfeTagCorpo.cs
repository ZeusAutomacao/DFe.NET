using System.Text;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.Wsdl.Corpo
{
    public class MdfeTagCorpo : ITagCorpo
    {
        public string GetTagCorpo(DFeCorpo dfeCorpo)
        {
            StringBuilder env = new StringBuilder();

            env.Append("<mdfeDadosMsg xmlns=\"");
            env.Append(dfeCorpo.NamespaceBody);
            env.Append("\">");
            env.Append(dfeCorpo.GetXml());
            env.Append("</mdfeDadosMsg>");

            return env.ToString();
        }
    }
}