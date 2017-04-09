using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte.XmlCte.Retorno
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "retEnviCte")]
    public class FusionRetornoEnvioCTe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public FusionTipoAmbienteCTe Ambiente { get; set; }

        [XmlElement(ElementName = "cUF")]
        public byte CodigoEstadoUf { get; set; }

        [XmlElement(ElementName = "verAplic")]
        public string VersaoAplicativo { get; set; }

        [XmlElement(ElementName = "cStat")]
        public short StatusResposta { get; set; }

        [XmlElement(ElementName = "xMotivo")]
        public string Motivo { get; set; }

        [XmlElement(ElementName = "infRec")]
        public FusionDadosReciboCTe DadosRecibo { get; set; }
    }

    [Serializable]
    public class FusionDadosReciboCTe
    {
        [XmlElement(ElementName = "nRec")]
        public string Numero { get; set; }

        [XmlElement(ElementName = "dhRecbto")]
        public DateTime? RecebidoEm { get; set; }

        [XmlElement(ElementName = "tMed")]
        public string TempoMedioResposta { get; set; }
    }
}