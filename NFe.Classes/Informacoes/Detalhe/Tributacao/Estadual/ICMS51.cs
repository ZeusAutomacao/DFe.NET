using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual
{
    public class ICMS51 : ICMSBasico
    {
        private decimal? _pRedBc;
        private decimal? _vBc;
        private decimal? _pIcms;
        private decimal? _vIcmsOp;
        private decimal? _pDif;
        private decimal? _vIcmsDif;
        private decimal? _vIcms;
        private decimal? _vBcfcp;
        private decimal? _pFcp;
        private decimal? _vFcp;
        private decimal? _pFCPDif;
        private decimal? _vFCPDif;
        private decimal? _vFCPEfet;

        /// <summary>
        ///     N11 - Origem da Mercadoria
        /// </summary>
        [XmlElement(Order = 1)]
        public OrigemMercadoria orig { get; set; }

        /// <summary>
        ///     N12- Situação Tributária
        /// </summary>
        [XmlElement(Order = 2)]
        public Csticms CST { get; set; }

        /// <summary>
        ///     N13 - Modalidade de determinação da BC do ICMS
        /// </summary>
        [XmlElement(Order = 3)]
        public DeterminacaoBaseIcms? modBC { get; set; }

        /// <summary>
        ///     N14 - Percentual de redução da BC
        /// </summary>
        [XmlElement(Order = 4)]
        public decimal? pRedBC
        {
            get { return _pRedBc.Arredondar(4); }
            set { _pRedBc = value.Arredondar(4); }
        }
        
        /// <summary>
        ///     N14a - Código de Benefício Fiscal na UF aplicado ao item quando houver RBC.
        /// </summary>
        [XmlElement(Order = 5)]
        public string cBenefRBC { get; set; }

        /// <summary>
        ///     N15 - Valor da BC do ICMS
        /// </summary>
        [XmlElement(Order = 6)]
        public decimal? vBC
        {
            get { return _vBc.Arredondar(2); }
            set { _vBc = value.Arredondar(2); }
        }

        /// <summary>
        ///     N16 - Alíquota do imposto
        /// </summary>
        [XmlElement(Order = 7)]
        public decimal? pICMS
        {
            get { return _pIcms.Arredondar(4); }
            set { _pIcms = value.Arredondar(4); }
        }

        /// <summary>
        ///     N16a - Valor do ICMS da Operação
        /// </summary>
        [XmlElement(Order = 8)]
        public decimal? vICMSOp
        {
            get { return _vIcmsOp.Arredondar(2); }
            set { _vIcmsOp = value.Arredondar(2); }
        }

        /// <summary>
        ///     N16b - Percentual do diferimento
        /// </summary>
        [XmlElement(Order = 9)]
        public decimal? pDif
        {
            get { return _pDif.Arredondar(4); }
            set { _pDif = value.Arredondar(4); }
        }

        /// <summary>
        ///     N16c - Valor do ICMS diferido
        /// </summary>
        [XmlElement(Order = 10)]
        public decimal? vICMSDif
        {
            get { return _vIcmsDif.Arredondar(2); }
            set { _vIcmsDif = value.Arredondar(2); }
        }

        /// <summary>
        ///     N17 - Valor do ICMS
        /// </summary>
        [XmlElement(Order = 11)]
        public decimal? vICMS
        {
            get { return _vIcms.Arredondar(2); }
            set { _vIcms = value.Arredondar(2); }
        }

        /// <summary>
        /// N17a - Valor da Base de Cálculo do FCP
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 12)]
        public decimal? vBCFCP
        {
            get { return _vBcfcp.Arredondar(2); }
            set { _vBcfcp = value.Arredondar(2); }
        }

        public bool vBCFCPSpecified
        {
            get { return vBCFCP.HasValue; }
        }

        /// <summary>
        /// N17b - Percentual do Fundo de Combate à Pobreza (FCP)
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 13)]
        public decimal? pFCP
        {
            get { return _pFcp.Arredondar(4); }
            set { _pFcp = value.Arredondar(4); }
        }

        public bool pFCPSpecified
        {
            get { return pFCP.HasValue; }
        }

        /// <summary>
        /// N17c - Valor do Fundo de Combate à Pobreza (FCP)
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 14)]
        public decimal? vFCP
        {
            get { return _vFcp.Arredondar(2); }
            set { _vFcp = value.Arredondar(2); }
        }

        public bool vFCPSpecified
        {
            get { return vFCP.HasValue; }
        }

        /// <summary>
        /// N17d - Percentual do diferimento do ICMS relativo ao Fundo de Combate à Pobreza(FCP)
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 15)]
        public decimal? pFCPDif
        {
            get { return _pFCPDif.Arredondar(4); }
            set { _pFCPDif = value.Arredondar(4); }
        }

        public bool ShouldSerializepFCPDif()
        {
            return pFCPDif.HasValue;
        }

        /// <summary>
        /// N17e - Valor do ICMS relativo ao Fundo de Combate à Pobreza (FCP) diferido
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 16)]
        public decimal? vFCPDif
        {
            get { return _vFCPDif.Arredondar(2); }
            set { _vFCPDif = value.Arredondar(2); }
        }

        public bool ShouldSerializevFCPDif()
        {
            return vFCPDif.HasValue;
        }

        /// <summary>
        /// N17f - Valor efetivo do ICMS relativo ao Fundo de Combate à Pobreza(FCP)
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 17)]
        public decimal? vFCPEfet
        {
            get { return _vFCPEfet.Arredondar(2); }
            set { _vFCPEfet = value.Arredondar(2); }
        }

        public bool ShouldSerializevFCPEfet()
        {
            return vFCPEfet.HasValue;
        }

        public bool ShouldSerializemodBC()
        {
            return modBC.HasValue;
        }

        public bool ShouldSerializepRedBC()
        {
            return pRedBC.HasValue;
        }

        public bool ShouldSerializevBC()
        {
            return vBC.HasValue;
        }

        public bool ShouldSerializepICMS()
        {
            return pICMS.HasValue;
        }

        public bool ShouldSerializevICMSOp()
        {
            return vICMSOp.HasValue;
        }

        public bool ShouldSerializepDif()
        {
            return pDif.HasValue;
        }

        public bool ShouldSerializevICMSDif()
        {
            return vICMSDif.HasValue;
        }

        public bool ShouldSerializevICMS()
        {
            return vICMS.HasValue;
        }
    }
}