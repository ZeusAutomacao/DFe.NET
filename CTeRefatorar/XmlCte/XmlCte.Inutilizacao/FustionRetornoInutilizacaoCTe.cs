using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte.XmlCte.Inutilizacao
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "retInutCTe")]
    public class FustionRetornoInutilizacaoCTe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "infInut")]
        public FusionDadosRetornoCTe DadosRetorno { get; set; }

        [XmlElement(ElementName = "Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public FusionAssinaturaDigital AssinaturaDigital { get; set; }
    }

    [Serializable]
    public class FusionDadosRetornoCTe
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public FusionTipoAmbienteCTe Ambiente { get; set; }

        [XmlElement(ElementName = "verAplic")]
        public string VersaoAplicativo { get; set; }

        [XmlElement(ElementName = "cStat")]
        public short CodigoStatus { get; set; }

        [XmlElement(ElementName = "xMotivo")]
        public string Motivo { get; set; }

        [XmlElement(ElementName = "cUF")]
        public byte CodigoEstadoUf { get; set; }

        [XmlElement(ElementName = "ano")]
        public byte Ano { get; set; }

        [XmlElement(ElementName = "CNPJ")]
        public string Cnpj { get; set; }

        [XmlElement(ElementName = "mod")]
        public FusionTipoDocumentoFiscalCTe DocumentoFiscal { get; set; }

        [XmlElement(ElementName = "serie")]
        public short Serie { get; set; }

        [XmlElement(ElementName = "nCTIni")]
        public long NumeroInicial { get; set; }

        [XmlElement(ElementName = "nCTFin")]
        public long NumeroFinal { get; set; }

        [XmlElement(ElementName = "dhRecbto")]
        public DateTime? ProcessadoEm { get; set; }

        [XmlElement(ElementName = "nPort")]
        public string Protocolo { get; set; }
    }
}