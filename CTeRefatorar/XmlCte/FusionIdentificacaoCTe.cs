using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    public class FusionIdentificacaoCTe
    {
        [XmlElement(ElementName = "cUF")]
        public FusionEstadoUFCTe EstadoUF { get; set; }

        [XmlElement(ElementName = "cCT")]
        public long CodigoNumerico { get; set; }

        [XmlElement(ElementName = "CFOP")]
        public string Cfop { get; set; }

        [XmlElement(ElementName = "natOp")]
        public string NaturezaOperacao { get; set; }

        [XmlElement(ElementName = "forPag")]
        public FusionFormaPagamentoCTe FormaPagamento { get; set; }

        [XmlElement(ElementName = "mod")]
        public FusionTipoDocumentoFiscalCTe TipoDocumentoFiscal { get; set; }

        [XmlElement(ElementName = "serie")]
        public short Serie { get; set; }

        [XmlElement(ElementName = "nCT")]
        public long NumeroDocumento { get; set; }

        [XmlElement(ElementName = "dhEmi")]
        public string EmitidaEm { get; set; }

        [XmlElement(ElementName = "tpImp")]
        public FusionDacteCTe FusionDacteCTe { get; set; }

        [XmlElement(ElementName = "tpEmis")]
        public FusionTipoEmissaoCTe FusionTipoEmissaoCTe { get; set; }

        [XmlElement(ElementName = "cDV")]
        public byte DigitoVerificador { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public FusionTipoAmbienteCTe Ambiente { get; set; }

        [XmlElement(ElementName = "tpCTe")]
        public FusionTipoCTe TipoCTe { get; set; }

        [XmlElement(ElementName = "procEmi")]
        public FusionTipoProcessoEmissaoCTe TipoProcessoEmissao { get; set; }

        [XmlElement(ElementName = "verProc")]
        public string VersaoAplicativoEmissor { get; set; }

        [XmlElement(ElementName = "cMunEnv")]
        public long CodigoIbgeMunicipioEnvio { get; set; }

        [XmlElement(ElementName = "xMunEnv")]
        public string NomeMunicipioEnvio { get; set; }

        [XmlElement(ElementName = "UFEnv")]
        public string SiglaDeEnvioUF { get; set; }

        [XmlElement(ElementName = "modal")]
        public FusionModalCTe Modal { get; set; }

        [XmlElement(ElementName = "tpServ")]
        public FusionTipoServicoCTe TipoServico { get; set; }

        [XmlElement(ElementName = "cMunIni")]
        public long CodigoIbgeMunicipioInicioOperacao { get; set; }

        [XmlElement(ElementName = "xMunIni")]
        public string NomeMunicipioInicioOperacao { get; set; }

        [XmlElement(ElementName = "UFIni")]
        public string SiglaDeUfInicioOperacao { get; set; }

        [XmlElement(ElementName = "cMunFim")]
        public long CodigoIbgeMunicipioFimOperacao { get; set; }

        [XmlElement(ElementName = "xMunFim")]
        public string NomeMunicipioFimOperacao { get; set; }

        [XmlElement(ElementName = "UFFim")]
        public string SiglaDeUfFimOperacao { get; set; }

        [XmlElement(ElementName = "retira")]
        public FusionIndicadorRecebedorCTe IndicadorRecebedor { get; set; }

        [XmlElement(ElementName = "toma03")]
        public FusionIndicadorPapelTomadorCTe IndicadorPapelTomador { get; set; }

        [XmlElement(ElementName = "toma4")]
        public FusionTomadorOutrosCTe FusionTomadorOutrosCTe { get; set; }
    }
}