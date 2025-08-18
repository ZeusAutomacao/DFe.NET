namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gIBSUF
    {
        private decimal _pIbsUf;
        private decimal _vIbsUf;

        // UB18
        public decimal pIBSUF
        {
            get => _pIbsUf.Arredondar(4);
            set => _pIbsUf = value.Arredondar(4);
        }

        // UB21
        public gDif gDif { get; set; }

        // UB24
        public gDevTrib gDevTrib { get; set; }

        // UB26
        public gRed gRed { get; set; }

        // UB35
        public decimal vIBSUF
        {
            get => _vIbsUf.Arredondar(2);
            set => _vIbsUf = value.Arredondar(2);
        }
    }

    public class gDif
    {
        private decimal _pDif;
        private decimal _vDif;

        // UB22
        public decimal pDif
        {
            get => _pDif.Arredondar(4);
            set => _pDif = value.Arredondar(4);
        }

        // UB23
        public decimal vDif
        {
            get => _vDif.Arredondar(2);
            set => _vDif = value.Arredondar(2);
        }
    }

    public class gDevTrib
    {
        private decimal _vDevTrib { get; set; }

        // UB25
        public decimal vDevTrib
        {
            get => _vDevTrib.Arredondar(2);
            set => _vDevTrib = value.Arredondar(2);
        }
    }

    public class gRed
    {
        private decimal _pRedAliq;
        private decimal _pAliqEfet;

        // UB27
        public decimal pRedAliq
        {
            get => _pRedAliq.Arredondar(4);
            set => _pRedAliq = value.Arredondar(4);
        }

        // UB28
        public decimal pAliqEfet
        {
            get => _pAliqEfet.Arredondar(4);
            set => _pAliqEfet = value.Arredondar(4);
        }
    }
}