namespace NFe.Classes.Informacoes.Detalhe.ProdEspecifico
{
    public class CIDE
    {
        private decimal _qBcProd;
        private decimal _vAliqProd;
        private decimal _vCide;

        /// <summary>
        ///     LA08 - BC da CIDE
        /// </summary>
        public decimal qBCProd
        {
            get { return _qBcProd; }
            set { _qBcProd = value.Arredondar(4); }
        }

        /// <summary>
        ///     LA09 - Valor da alíquota da CIDE
        /// </summary>
        public decimal vAliqProd
        {
            get { return _vAliqProd; }
            set { _vAliqProd = value.Arredondar(4); }
        }

        /// <summary>
        ///     LA10 - Valor da CIDE
        /// </summary>
        public decimal vCIDE
        {
            get { return _vCide; }
            set { _vCide = value.Arredondar(2); }
        }
    }
}