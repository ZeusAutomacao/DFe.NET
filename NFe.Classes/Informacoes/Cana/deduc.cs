namespace NFe.Classes.Informacoes.Cana
{
    public class deduc
    {
        private decimal _vDed;

        /// <summary>
        ///     ZC11 - Descrição da Dedução
        /// </summary>
        public string xDed { get; set; }

        /// <summary>
        ///     ZC12 - Valor da Dedução
        /// </summary>
        public decimal vDed
        {
            get { return _vDed; }
            set { _vDed = value.Arredondar(2); }
        }
    }
}