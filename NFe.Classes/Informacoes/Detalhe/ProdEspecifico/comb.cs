using System.Collections.Generic;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.ProdEspecifico
{
    public class comb : ProdutoEspecifico
    {
        private decimal? _pMixGn;
        private decimal? _qTemp;
        private decimal? _pGlp;
        private decimal? _pGNn;
        private decimal? _pGNi;
        private decimal? _vPart;
        private decimal? _pBio;

        /// <summary>
        ///     LA02 - Código de produto da ANP
        /// Versão 3.00
        /// Versão 4.00
        /// </summary>
        public string cProdANP { get; set; }

        /// <summary>
        ///     LA03 - Percentual de Gás Natural para o produto GLP (cProdANP=210203001)
        /// Versão 3.00
        /// </summary>
        public decimal? pMixGN
        {
            get { return _pMixGn.Arredondar(4); }
            set { _pMixGn = value.Arredondar(4); }
        }

        public bool pMixGNSpecified
        {
            get { return pMixGN.HasValue; }
        }

        /// <summary>
        /// LA03 - Descrição do produto conforme ANP
        /// Versão 4.00
        /// </summary>
        public string descANP { get; set; }

        /// <summary>
        /// LA03a - Percentual do GLP derivado do petróleo no produto GLP (cProdANP=210203001)
        /// Versão 4.00
        /// </summary>
        public decimal? pGLP
        {
            get { return _pGlp.Arredondar(4); }
            set { _pGlp = value.Arredondar(4); }
        }

        public bool pGLPSpecified
        {
            get { return pGLP.HasValue; }
        }

        /// <summary>
        /// LA03b - Percentual de Gás Natural Nacional – GLGNn para o produto GLP (cProdANP= 210203001)
        /// Versão 4.00
        /// </summary>
        public decimal? pGNn
        {
            get { return _pGNn.Arredondar(4); }
            set { _pGNn = value.Arredondar(4); }
        }

        public bool pGNnSpecified
        {
            get { return pGNn.HasValue; }
        }

        /// <summary>
        /// LA03c - Percentual de Gás Natural Importado – GLGNi para o produto GLP (cProdANP= 210203001)
        /// Versão 4.00
        /// </summary>
        public decimal? pGNi
        {
            get { return _pGNi.Arredondar(4); }
            set { _pGNi = value.Arredondar(4); }
        }

        public bool pGNiSpecified
        {
            get { return pGNi.HasValue; }
        }

        /// <summary>
        /// LA03d - Valor de partida (cProdANP=210203001)
        /// Versão 4.00
        /// </summary>
        public decimal? vPart
        {
            get { return _vPart.Arredondar(2); }
            set { _vPart = value.Arredondar(2); }
        }

        public bool vPartSpecified
        {
            get { return vPart.HasValue; }
        }

        /// <summary>
        ///     LA04 - Código de autorização / registro do CODIF
        /// </summary>
        public string CODIF { get; set; }

        /// <summary>
        ///     LA05 - Quantidade de combustível faturada à temperatura ambiente
        /// </summary>
        public decimal? qTemp
        {
            get { return _qTemp.Arredondar(4); }
            set { _qTemp = value.Arredondar(4); }
        }

        public bool qTempSpecified
        {
            get { return qTemp.HasValue; }
        }

        /// <summary>
        ///     LA06 - Sigla da UF de consumo
        /// </summary>
        public string UFCons { get; set; }

        /// <summary>
        ///     LA07 - Informações da CIDE
        /// </summary>
        public CIDE CIDE { get; set; }

        /// <summary>
        /// LA11 - Informações do grupo de “encerrante”
        /// </summary>
        public encerrante encerrante { get; set; }

        /// <summary>
        /// LA17 - Percentual do índice de mistura do Biodiesel (B100) no Óleo Diesel B instituído pelo órgão regulamentador
        /// </summary>
        public decimal? pBio
        {
            get { return _pBio.Arredondar(4); }
            set { _pBio = value.Arredondar(4); }
        }

        /// <summary>
        /// LA18 - Grupo indicador da origem do combustível
        /// </summary>
        [XmlElement("origComb")]
        public List<origComb> origComb { get; set; }

        public bool ShouldSerializepMixGN()
        {
            return pMixGN.HasValue;
        }

        public bool ShouldSerializeqTemp()
        {
            return qTemp.HasValue;
        }

        public bool ShouldSerializepBio()
        {
            return pBio.HasValue;
        }
    }
}