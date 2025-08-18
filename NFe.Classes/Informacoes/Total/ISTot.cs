namespace NFe.Classes.Informacoes.Total
{
    public class ISTot
    {
        private decimal _vIS;

        // W33
        public decimal vIS
        {
            get => _vIS;
            set => _vIS = value.Arredondar(2);
        }
    }
}