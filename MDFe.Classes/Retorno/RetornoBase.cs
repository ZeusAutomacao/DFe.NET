using System.Xml.Serialization;

namespace MDFe.Classes.Retorno
{
    public class RetornoBase
    {
        [XmlIgnore]
        public string EnvioXmlString { get; set; }
        [XmlIgnore]
        public string RetornoXmlString { get; set; }
    }
}