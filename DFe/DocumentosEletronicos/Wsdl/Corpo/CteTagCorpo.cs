using System.Text;

namespace DFe.DocumentosEletronicos.Wsdl.Corpo
{
    public class CteTagCorpo : ITagCorpo
    {
        public string GetTagCorpo(DFeSoapConfig soapConfig)
        {
            StringBuilder env = new StringBuilder();

            env.Append("<cteDadosMsg xmlns=\"");
            env.Append(soapConfig.NamespaceBody);
            env.Append("\">");
            env.Append(soapConfig.Xml.LastChild.OuterXml);
            env.Append("</cteDadosMsg>");

            return env.ToString();
        }
    }
}