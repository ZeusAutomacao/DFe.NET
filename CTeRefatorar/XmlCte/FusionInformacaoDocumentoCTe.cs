using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    public class FusionInformacaoDocumentoCTe
    {
        [XmlElement(ElementName = "infNFe")]
        public List<FusionDocumentosNFe> DocumentosNFe { get; set; }

        [XmlElement(ElementName = "infOutros")]
        public List<FusionDocuemntoOutroCTe> DocumentosOutros { get; set; }

        [XmlElement(ElementName = "infNF")]
        public List<FusionDocumentoImpressoCTe> DocumentosImpressos { get; set; }

        public FusionInformacaoDocumentoCTe()
        {
            DocumentosNFe = new List<FusionDocumentosNFe>();
            DocumentosOutros = new List<FusionDocuemntoOutroCTe>();
            DocumentosImpressos = new List<FusionDocumentoImpressoCTe>();
        }
    }

    [Serializable]
    public class FusionDocumentoImpressoCTe
    {
        [XmlElement(ElementName = "nRoma")]
        public string NumeroRomaneiro { get; set; }

        [XmlElement(ElementName = "nPed")]
        public string NumeroPedido { get; set; }

        [XmlElement(ElementName = "mod")]
        public FusionModeloNotaFiscalCTe ModeloNotaFiscal { get; set; }

        [XmlElement(ElementName = "serie")]
        public short Serie { get; set; }

        [XmlElement(ElementName = "nDoc")]
        public string Numero { get; set; }

        [XmlIgnore]
        public DateTime DataEmissao { get; set; }

        [XmlElement(ElementName = "dEmi")]
        public string ProxyDataEmissao
        {
            get { return DataEmissao.ToString("yyyy-MM-dd"); }
            set { DataEmissao = DateTime.Parse(value); }
        }

        [XmlElement(ElementName = "vBC")]
        public decimal BaseCacluloIcms { get; set; }

        [XmlElement(ElementName = "vICMS")]
        public decimal IcmsTotal { get; set; }

        [XmlElement(ElementName = "vBCST")]
        public decimal BaseCalculoIcmsSt { get; set; }

        [XmlElement(ElementName = "vST")]
        public decimal IcmsStTotal { get; set; }

        [XmlElement(ElementName = "vProd")]
        public decimal ValorTotalProdutos { get; set; }

        [XmlElement(ElementName = "vNF")]
        public decimal ValorTotalNF { get; set; }

        [XmlElement(ElementName = "nCFOP")]
        public string CfopPredominante { get; set; }

        [XmlElement(ElementName = "nPeso")]
        public decimal? PesoTotalEmKg { get; set; }

        [XmlElement(ElementName = "PIN")]
        public string PinSuframa { get; set; }

        [XmlIgnore]
        public DateTime? DataPrevista { get; set; }

        [XmlElement(ElementName = "dPrev")]
        public string ProxyDataPrevista
        {
            get { return DataPrevista?.ToString("yyyy-MM-dd"); }
            set { DataPrevista = DateTime.Parse(value); }
        }

        public bool PesoTotalEmKgSpecified => PesoTotalEmKg.HasValue;
    }

    [Serializable]
    public class FusionDocuemntoOutroCTe
    {
        [XmlIgnore]
        public DateTime? DataEmissao { get; set; }

        [XmlIgnore]
        public DateTime? DataPrevisaoEntrega { get; set; }

        [XmlElement(ElementName = "tpDoc")]
        public FusionTipoDocumentoOriginarioCTe TipoDocumentoOriginario { get; set; }

        [XmlElement(ElementName = "descOutros")]
        public string DescricaoOutros { get; set; }

        [XmlElement(ElementName = "nDoc")]
        public string Numero { get; set; }

        [XmlElement(ElementName = "dEmi")]
        public string ProxyDataEmissao
        {
            get { return DataEmissao?.ToString("yyyy-MM-dd"); }
            set { DataEmissao = DateTime.Parse(value); }
        }

        [XmlElement(ElementName = "vDocFisc")]
        public decimal Valor { get; set; }

        [XmlElement(ElementName = "dPrev")]
        public string ProxyDataPrevisaoEntrega
        {
            get { return DataPrevisaoEntrega?.ToString("yyyy-MM-dd"); }
            set { DataPrevisaoEntrega = DateTime.Parse(value); }
        }
    }

    [Serializable]
    public class FusionDocumentosNFe
    {
        [XmlElement(ElementName = "chave")]
        public string Chave { get; set; }

        [XmlElement(ElementName = "PIN")]
        public string PinSuframa { get; set; }

        [XmlElement(ElementName = "dPrev")]
        public string DataPrevistaEntrega { get; set; }
    }
}