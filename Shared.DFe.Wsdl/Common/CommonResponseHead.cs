using System.Xml.Serialization;

namespace DFe.DocumentosEletronicos.Common
{
    [XmlRoot(ElementName = "Header", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public abstract class CommonResponseHead
    {
    }
}