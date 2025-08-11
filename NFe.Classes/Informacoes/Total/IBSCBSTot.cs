namespace NFe.Classes.Informacoes.Total
{
    public class IBSCBSTot
    {
        private decimal _vBCIBSCBS;

        // W35
        public decimal vBCIBSCBS
        {
            get => _vBCIBSCBS;
            set => _vBCIBSCBS = value.Arredondar(2);
        }

        // W36
        public gIBS gIBS { get; set; }

        // W50
        public gCBSTotal gCBS { get; set; }

        // W57
        public gMono gMono { get; set; }
    }
}