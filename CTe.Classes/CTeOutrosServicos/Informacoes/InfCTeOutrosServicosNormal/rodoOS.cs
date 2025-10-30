using System.Xml.Serialization;
using CTe.Classes.Informacoes.infCTeNormal.infModals;
using CTe.Classes.Informacoes.infCTeNormal.infModals.rodoviarioOS;

namespace CTe.CTeOSDocumento.CTe.CTeOS.Informacoes.InfCTeNormal
{
    public class rodoOS : ContainerModal
    {
        public string TAF { get; set; }

        public string NroRegEstadual { get; set; }

        [XmlElement(ElementName = "veic")]
        public veicOs veic { get; set; }

        public infFretamento infFretamento { get; set; }
    }
}