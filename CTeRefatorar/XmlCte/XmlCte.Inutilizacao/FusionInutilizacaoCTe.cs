using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte.XmlCte.Inutilizacao
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "inutCTe")]
    public class FusionInutilizacaoCTe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "infInut")]
        public FusionDadosDoPedidoCTe DadosDoPedido { get; set; }

        [XmlElement(ElementName = "Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public FusionAssinaturaDigital AssinaturaDigital { get; set; }

        public FusionInutilizacaoCTe()
        {
            DadosDoPedido = new FusionDadosDoPedidoCTe();
        }
    }

    [Serializable]
    public class FusionDadosDoPedidoCTe
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public FusionTipoAmbienteCTe Ambiente { get; set; }

        [XmlElement(ElementName = "xServ")]
        public string Servico { get; set; } = "INUTILIZAR";

        [XmlElement(ElementName = "cUF")]
        public byte CodigoEstadoUf { get; set; }

        [XmlElement(ElementName = "ano")]
        public byte Ano { get; set; }

        [XmlElement(ElementName = "CNPJ")]
        public string Cnpj { get; set; }

        [XmlElement(ElementName = "mod")]
        public FusionTipoDocumentoFiscalCTe TipoDocumentoFiscal { get; set; }

        [XmlElement(ElementName = "serie")]
        public short Serie { get; set; }

        [XmlElement(ElementName = "nCTIni")]
        public long NumeroInicial { get; set; }

        [XmlElement(ElementName = "nCTFin")]
        public long NumeroFinal { get; set; }

        [XmlElement(ElementName = "xJust")]
        public string Justificativa { get; set; }
    }
}