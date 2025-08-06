namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gIBSMun
    {
        private decimal _pIbsMun;
        private decimal _vIbsMun;

        // UB37
        public decimal pIBSMun
        {
            get => _pIbsMun.Arredondar(4);
            set => _pIbsMun = value.Arredondar(4);
        }

        // UB40
        public gDif gDif { get; set; }

        // UB43
        public gDevTrib gDevTrib { get; set; }

        // UB45
        public gRed gRed { get; set; }

        // UB54
        public decimal vIBSMun
        {
            get => _vIbsMun.Arredondar(2);
            set => _vIbsMun = value.Arredondar(2);
        }
    }
}