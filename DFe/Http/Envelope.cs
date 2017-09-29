using System.Text;
using System.Xml;

namespace DFe.Http
{
    public class Envelope
    {
        public string Construir(XmlNode xml)
        {
            StringBuilder env = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");


            env.Append("<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ");
            env.Append("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" ");
            env.Append("xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">");

            env.Append("<soap12:Header>");
            env.Append("<nfeCabecMsg xmlns=\"http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico\">");
            env.Append("<cUF>29</cUF>");
            env.Append("<versaoDados>3.10</versaoDados>");
            env.Append("</nfeCabecMsg>");
            env.Append("</soap12:Header>");

            env.Append("<soap12:Body>");

            env.Append("<nfeDadosMsg xmlns=\"http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico\">");
            env.Append(xml.LastChild.OuterXml.ToString());
            env.Append("</nfeDadosMsg>");

            env.Append("</soap12:Body>");
            env.Append("</soap12:Envelope>");

            return env.ToString();
        }
    }
}