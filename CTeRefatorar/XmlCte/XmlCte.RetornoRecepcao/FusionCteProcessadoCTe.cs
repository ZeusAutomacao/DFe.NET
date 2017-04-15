using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte.XmlCte.RetornoRecepcao
{
    [Serializable]
    public class FusionCteProcessadoCTe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "infProt")]
        public FusionInformacaoProcessadaCTe Informacoes { get; set; }
    }

    [Serializable]
    public class FusionInformacaoProcessadaCTe
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public FusionTipoAmbienteCTe Ambiente { get; set; }

        [XmlElement(ElementName = "verAplic")]
        public string VersaoAplicativo { get; set; }

        [XmlElement(ElementName = "dhRecbto")]
        public DateTime? ProcessadoEm { get; set; }

        [XmlElement(ElementName = "nProt")]
        public string NumeroProtocolo { get; set; }

        [XmlElement(ElementName = "digVal")]
        public string DigestVal { get; set; }

        [XmlElement(ElementName = "cStat")]
        public short CodigoStatusResposta { get; set; }

        [XmlElement(ElementName = "xMotivo")]
        public string Motivo { get; set; }

        [XmlElement(ElementName = "Signature")]
        public FusionAssinaturaDigital AssinaturaDigital { get; set; }

        public bool Autorizado => CodigoStatusResposta == 100;
    }
}