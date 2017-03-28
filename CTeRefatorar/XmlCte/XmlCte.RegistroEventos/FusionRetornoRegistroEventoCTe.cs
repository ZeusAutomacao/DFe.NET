using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte.XmlCte.RegistroEventos
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "retEventoCTe")]
    public class FusionRetornoRegistroEventoCTe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; } = "2.00";

        [XmlElement(ElementName = "infEvento")]
        public FusionRetornoInformacaoEventoCTe RetornoInformacaoEvento { get; set; }

        [XmlElement(ElementName = "Signature")]
        public FusionAssinaturaDigital AssinaturaDigital { get; set; }
    }

    [Serializable]
    public class FusionRetornoInformacaoEventoCTe
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public FusionTipoAmbienteCTe Ambiente { get; set; }

        [XmlElement(ElementName = "verAplic")]
        public string VersaoAplicacao { get; set; }

        [XmlElement(ElementName = "cOrgao")]
        public byte OrgaoEmissor { get; set; }

        [XmlElement(ElementName = "cStat")]
        public short CodigoStatus { get; set; }

        [XmlElement(ElementName = "xMotivo")]
        public string Motivo { get; set; }

        [XmlElement(ElementName = "chCTe")]
        public string Chave { get; set; }

        [XmlElement(ElementName = "tpEvento")]
        public int TipoEvento { get; set; }

        [XmlElement(ElementName = "xEvento")]
        public string DescricaoEvento { get; set; }

        [XmlElement(ElementName = "nSeqEvento")]
        public byte SequencialEvento { get; set; }

        [XmlElement(ElementName = "dhRegEvento")]
        public DateTime? DataEHoraRegistroEvento { get; set; }

        [XmlElement(ElementName = "nProt")]
        public string NumeroProtocolo { get; set; }
    }
}