using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte.XmlCte.ConsultaStatus
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "retConsStatServCte")]
    public class FusionRetornoConsultaStatusCTe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public FusionTipoAmbienteCTe Ambiente { get; set; }

        [XmlElement(ElementName = "verAplic")]
        public string VersaoAplicacao { get; set; }

        [XmlElement(ElementName = "cStat")]
        public short CodigoStatus { get; set; }

        [XmlElement(ElementName = "xMotivo")]
        public string Motivo { get; set; }

        [XmlElement(ElementName = "cUF")]
        public byte CodigoEstadoUf { get; set; }

        [XmlElement(ElementName = "dhRecbto")]
        public DateTime RecebeuEm { get; set; }

        [XmlElement(ElementName = "tMed")]
        public int? TempoMedio { get; set; }

        [XmlElement(ElementName = "dhRetorno")]
        public DateTime? PrevisaoRetornoEm { get; set; }

        [XmlElement(ElementName = "xObs")]
        public string Observacao { get; set; }
    }
}