namespace NFe.Classes.Informacoes.Total
{
    public class ISTot
    {
        private decimal _vIS;
        
        /// <summary>
        ///     W33 - Total do imposto seletivo
        /// </summary>
        public decimal vIS
        {
            get => _vIS;
            set => _vIS = value.Arredondar(2);
        }
    }
}