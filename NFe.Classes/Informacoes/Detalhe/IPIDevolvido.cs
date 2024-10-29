namespace NFe.Classes.Informacoes.Detalhe
{
    public class IPIDevolvido
    {
        private decimal _vIpiDevol;

        /// <summary>
        ///     UA04 - Valor do IPI devolvido
        /// </summary>
        public decimal vIPIDevol
        {
            get { return _vIpiDevol; }
            set { _vIpiDevol = value.Arredondar(2); }
        }
    }
}