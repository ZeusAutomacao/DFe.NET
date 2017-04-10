using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte.XmlCte.ConsultaStatusServico
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "consStatServCte")]
    public class FusionConsultaStatusServicoCTe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; } = "2.00";

        [XmlElement(ElementName = "tpAmb")]
        public FusionTipoAmbienteCTe Ambiente { get; set; }

        [XmlElement(ElementName = "xServ")]
        public string ServicoSolicitado { get; set; } = "STATUS";
    }
}