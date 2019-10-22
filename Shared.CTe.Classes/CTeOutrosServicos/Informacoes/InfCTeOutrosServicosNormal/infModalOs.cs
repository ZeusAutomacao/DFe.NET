using System.Xml.Serialization;
using CTe.Classes.Informacoes.infCTeNormal.infModals;
using CTe.Classes.Informacoes.Tipos;

namespace CTe.CTeOSDocumento.CTe.CTeOS.Informacoes.InfCTeNormal
{
    public class infModalOs
    {
        [XmlAttribute]
        public versaoModal versaoModal { get; set; }

        [XmlElement("rodoOS", typeof(rodoOS), Namespace = "http://www.portalfiscal.inf.br/cte")]
        public ContainerModal ContainerModal { get; set; }
    }
}