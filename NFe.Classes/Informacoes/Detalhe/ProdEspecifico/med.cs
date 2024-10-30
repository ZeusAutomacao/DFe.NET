using System;
using System.Xml.Serialization;
using DFe.Utils;

namespace NFe.Classes.Informacoes.Detalhe.ProdEspecifico
{
    public class med : ProdutoEspecifico
    {
        private decimal? _qLote;
        private decimal _vPmc;

        /// <summary>
        /// K01a - Código de Produto da ANVISA
        /// VERSÃO 4.00
        /// </summary>
        public string cProdANVISA { get; set; }

        public string xMotivoIsencao { get; set; }

        /// <summary>
        ///     K02 - Número do Lote de medicamentos ou de matérias-primas farmacêuticas
        /// VERSÃO 3.00
        /// </summary>
        public string nLote { get; set; }

        /// <summary>
        ///     K03 - Quantidade de produto no Lote de medicamentos ou de matérias-primas farmacêuticas
        /// Versão 3.00
        /// </summary>
        public decimal? qLote
        {
            get { return _qLote; }
            set { _qLote = value.Arredondar(3); }
        }

        public bool qLoteSpecified
        {
            get { return qLote.HasValue; }
        }

        /// <summary>
        ///     K04 - Data de fabricação.
        /// Versão 3.00
        /// </summary>
        [XmlIgnore]
        public DateTime? dFab { get; set; }

        /// <summary>
        /// Proxy para dFab no formato AAAA-MM-DD
        /// </summary>
        [XmlElement(ElementName = "dFab")]
        public string ProxydFab
        {
            get { return dFab.ParaDataString(); }
            set { dFab = DateTime.Parse(value); }
        }

        public bool ProxydFabSpecified
        {
            get { return dFab.HasValue; }
        }

        /// <summary>
        ///     K05 - Data de validade.
        /// Versão 3.00
        /// </summary>
        [XmlIgnore]
        public DateTime? dVal { get; set; }

        /// <summary>
        /// Proxy para dVal no formato AAAA-MM-DD
        /// Versão 3.00
        /// </summary>
        [XmlElement(ElementName = "dVal")]
        public string ProxydVal
        {
            get { return dVal.ParaDataString(); }
            set { dVal = DateTime.Parse(value); }
        }

        public bool ProxydValSpecified
        {
            get { return dVal.HasValue; }
        }

        /// <summary>
        ///     K06 - Preço máximo consumidor
        /// Versão 3.00
        /// Versão 4.00
        /// </summary>
        public decimal vPMC
        {
            get { return _vPmc; }
            set { _vPmc = value.Arredondar(2); }
        }
    }
}