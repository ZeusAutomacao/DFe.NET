using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte.XmlCte.ConsultaStatus
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "consStatServCte")]
    public class FusionConsultaStatusCTe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public FusionTipoAmbienteCTe Ambiente { get; set; }

        [XmlElement(ElementName = "xServ")]
        public string ServicoSolicitado { get; set; } = "STATUS";
    }
}