namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gCBS
    {
        private decimal _pGBS;
        private decimal _vGBS;

        // UB37
        public decimal pCBS
        {
            get => _pGBS.Arredondar(4);
            set => _pGBS = value.Arredondar(4);
        }

        // UB40
        public gDif gDif { get; set; }

        // UB43
        public gDevTrib gDevTrib { get; set; }

        // UB45
        public gRed gRed { get; set; }

        // UB67
        public decimal vCBS
        {
            get => _vGBS.Arredondar(2);
            set => _vGBS = value.Arredondar(2);
        }
    }
}