namespace NFe.Classes.Informacoes.Detalhe.ProdEspecifico
{
    public class encerrante
    {
        private decimal _vEncIni;
        private decimal _vEncFin;
        private decimal? _pBio;

        /// <summary>
        /// LA12 - Número de identificação do bico utilizado no abastecimento
        /// </summary>
        public int nBico { get; set; }

        /// <summary>
        /// LA13 - Número de identificação da bomba ao qual o bico está interligado
        /// </summary>
        public int? nBomba { get; set; }
        public bool ShouldSerializenBomba()
        {
            return nBomba.HasValue;
        }

        /// <summary>
        /// LA14 - Número de identificação do tanque ao qual o bico está interligado
        /// </summary>
        public int nTanque { get; set; }

        /// <summary>
        /// LA15 - Valor do Encerrante no início do abastecimento
        /// </summary>
        public decimal vEncIni
        {
            get { return _vEncIni; }
            set { _vEncIni = value.Arredondar(3); }
        }

        /// <summary>
        /// LA16 - Valor do Encerrante no final do abastecimento
        /// </summary>
        public decimal vEncFin
        {
            get { return _vEncFin; }
            set { _vEncFin = value.Arredondar(3); }
        }

        /// <summary>
        /// LA17 - Percentual do índice de mistura do Biodiesel (B100) no Óleo Diesel B instituído pelo órgão regulamentador
        /// </summary>
        public decimal? pBio
        {
            get { return _pBio; }
            set { _pBio = value.HasValue ? value.Arredondar(3) : null; }
        }
        public bool ShouldSerializepBio()
        {
            return pBio.HasValue;
        }

    }
}