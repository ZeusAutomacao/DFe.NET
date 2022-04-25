using System.Text;
using CTe.CTeOSDocumento.Wsdl;

namespace DFe.Http
{
    public class Envelope
    {
        public static string Construir(DFeSoapConfig soapConfig)
        {
            StringBuilder env = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");


            env.Append("<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ");
            env.Append("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" ");
            env.Append("xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">");

            // cabecalho
            env.Append("<soap12:Header>");
            env.Append(soapConfig.DFeCabecalho.GetXmlHeader());
            env.Append("</soap12:Header>");


            // corpo
            env.Append("<soap12:Body>");
            env.Append(soapConfig.DFeCorpo.GetXmlBody());
            env.Append("</soap12:Body>");


            env.Append("</soap12:Envelope>");

            return env.ToString();
        }
    }
}