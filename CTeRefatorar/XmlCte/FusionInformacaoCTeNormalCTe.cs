using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    public class FusionInformacaoCTeNormalCTe
    {
        [XmlElement(ElementName = "infCarga")]
        public FusionInformacaoCargaCTe InformacaoCarga { get; set; }

        [XmlElement(ElementName = "infDoc")]
        public FusionInformacaoDocumentoCTe InformacaoDocumento { get; set; }

        [XmlElement(ElementName = "seg")]
        public List<FusionSeguroCTe> InfoSeguroDaCarga { get; set; }

        [XmlElement(ElementName = "infModal")]
        public FusionInformacaoModalCTe Modal { get; set; }

        [XmlElement(ElementName = "peri")]
        public List<FusionProdutoPerigosoCTe> ProdutosPerigosos { get; set; }

        [XmlElement(ElementName = "veicNovos")]
        public List<FusionVeiculoTransportadoCTe> VeiculosTransportados { get; set; }

        public FusionInformacaoCTeNormalCTe()
        {
            InformacaoCarga = new FusionInformacaoCargaCTe();
            InformacaoDocumento = new FusionInformacaoDocumentoCTe();
            InfoSeguroDaCarga = new List<FusionSeguroCTe>();
            Modal = new FusionInformacaoModalCTe();
            ProdutosPerigosos = new List<FusionProdutoPerigosoCTe>();
            VeiculosTransportados = new List<FusionVeiculoTransportadoCTe>();
        }
    }

    [Serializable]
    public class FusionInformacaoCargaCTe
    {
        private string _outrasCaracteristicas;
        private decimal? _valorTotalCarga;

        [XmlElement(ElementName = "vCarga")]
        public decimal? ValorTotalCarga
        {
            get { return _valorTotalCarga; }
            set { _valorTotalCarga = value; }
        }

        [XmlElement(ElementName = "proPred")]
        public string NomeProdutoPredominante { get; set; }

        [XmlElement(ElementName = "xOutCat")]
        public string OutrasCaracteristicas
        {
            get { return _outrasCaracteristicas; }
            set { _outrasCaracteristicas = value; }
        }

        [XmlElement(ElementName = "infQ")]
        public List<FusionInformacoesQuantidadeDaCargaCTe> InformacoesQuantidadeDaCarga { get; set; }

        public bool OutrasCaracteristicasSpecified
        {
            get { return !string.IsNullOrEmpty(_outrasCaracteristicas); }
        }

        public bool ValorTotalCargaSpecified => _valorTotalCarga.HasValue;

        public FusionInformacaoCargaCTe()
        {
            InformacoesQuantidadeDaCarga = new List<FusionInformacoesQuantidadeDaCargaCTe>();
        }
    }

    [Serializable]
    public class FusionInformacoesQuantidadeDaCargaCTe
    {
        [XmlElement(ElementName = "cUnid")]
        public FusionUnidadeMedidaCTe UnidadeMedida { get; set; }

        [XmlElement(ElementName = "tpMed")]
        public string DescricaoMedida { get; set; }

        [XmlElement(ElementName = "qCarga")]
        public decimal Quantidade { get; set; }
    }
}