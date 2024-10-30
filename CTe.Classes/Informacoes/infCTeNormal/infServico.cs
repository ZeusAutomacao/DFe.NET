using System.Xml.Serialization;
using CTe.CTeOSDocumento.CTe.CTeOS.Informacoes.InfCTeNormal;

namespace CTe.Classes.Informacoes.infCTeNormal
{
    public class infServico
    {
        public string xDescServ { get; set; }

        [XmlElement(ElementName = "infQ")]
        public infQOs infQ { get; set; }
    }
}