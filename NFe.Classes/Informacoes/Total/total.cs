namespace NFe.Classes.Informacoes.Total
{
    public class total
    {
        private decimal? _vNFTot;

        /// <summary>
        ///     W02 - Grupo Totais referentes ao ICMS
        /// </summary>
        public ICMSTot ICMSTot { get; set; }

        /// <summary>
        ///     W17 - Grupo Totais referentes ao ISSQN
        /// </summary>
        public ISSQNtot ISSQNtot { get; set; }

        /// <summary>
        ///     W23 - Grupo Retenções de Tributos
        /// </summary>
        public retTrib retTrib { get; set; }

        // W31
        public ISTot ISTot { get; set; }

        // W34
        public IBSCBSTot IBSCBSTot { get; set; }

        // W60
        public decimal? vNFTot
        {
            get => _vNFTot;
            set => _vNFTot = value.Arredondar(2);
        }

        public bool ShouldSerializevNFTot()
        {
            return _vNFTot.HasValue;
        }
    }
}