using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte.XmlCte.ConsultaStatusServico
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "retConsStatServCte")]
    public class FusionRetornoConsultaStatusServicoCTe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; } = "2.00";

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
        public DateTime DataEHoraRecebimento { get; set; }

        [XmlElement(ElementName = "tMed")]
        public int? TempoMedioResposta { get; set; }

        [XmlElement(ElementName = "dhRetorno")]
        public DateTime? DataEHoraRetorno { get; set; }

        [XmlElement(ElementName = "xObs")]
        public string InformacoesAdicionais { get; set; }
    }
}