using System.Xml.Serialization;

namespace CTe.Classes.Servicos
{
    public abstract class RetornoBase
    {
        [XmlIgnore]
        public string EnvioXmlString { get; set; }
        [XmlIgnore]
        public string RetornoXmlString { get; set; }
    }
}