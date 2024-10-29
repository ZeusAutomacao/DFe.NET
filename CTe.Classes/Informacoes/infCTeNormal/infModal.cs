using System.Xml.Serialization;
using CTe.Classes.Informacoes.infCTeNormal.infModals;
using CTe.Classes.Informacoes.Tipos;

namespace CTe.Classes.Informacoes.infCTeNormal
{
    public class infModal
    {
        [XmlAttribute]
        public versaoModal versaoModal { get; set; }

        [XmlElement("rodo", typeof(rodo), Namespace = "http://www.portalfiscal.inf.br/cte")]
        [XmlElement("rodoOS", typeof(rodoOS), Namespace = "http://www.portalfiscal.inf.br/cte")]
        [XmlElement("aereo", typeof(aereo), Namespace = "http://www.portalfiscal.inf.br/cte")]
        [XmlElement("aquav", typeof(aquav), Namespace = "http://www.portalfiscal.inf.br/cte")]
        [XmlElement("ferrov", typeof(ferrov), Namespace = "http://www.portalfiscal.inf.br/cte")]
        [XmlElement("duto", typeof(duto), Namespace = "http://www.portalfiscal.inf.br/cte")]
        [XmlElement("multimodal", typeof(multimodal), Namespace = "http://www.portalfiscal.inf.br/cte")]
        public ContainerModal ContainerModal { get; set; }

    }
}