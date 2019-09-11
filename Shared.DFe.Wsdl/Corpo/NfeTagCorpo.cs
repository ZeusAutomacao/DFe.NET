using System.Text;
using DFe.Wsdl;

namespace CTe.CTeOSDocumento.Wsdl.Corpo
{
    public class NfeTagCorpo : ITagCorpo
    {
        private readonly string _parametroEntrada;

        public NfeTagCorpo(string parametroEntrada)
        {
            _parametroEntrada = parametroEntrada;
        }

        public string GetTagCorpo(DFeCorpo dfeCorpo)
        {
            StringBuilder env = new StringBuilder();

            env.Append("<");
            env.Append(_parametroEntrada);
            env.Append(" xmlns=\"");
            env.Append(dfeCorpo.NamespaceBody);
            env.Append("\">");
            env.Append(dfeCorpo.GetXml());
            env.Append("</");
            env.Append(_parametroEntrada);
            env.Append(">");

            return env.ToString();
        }
    }
}