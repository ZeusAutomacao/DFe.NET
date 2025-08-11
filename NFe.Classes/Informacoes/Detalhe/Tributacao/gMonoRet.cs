namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gMonoRet
    {
        private decimal _qBcMonoRet;
        private decimal _adRemIbsRet;
        private decimal _vIbsMonoRet;
        private decimal _adRemCbsRet;
        private decimal _vCbsMonoRet;

        // UB95
        public decimal qBCMonoRet
        {
            get => _qBcMonoRet.Arredondar(4);
            set => _qBcMonoRet = value.Arredondar(4);
        }

        // UB96
        public decimal adRemIBSRet
        {
            get => _adRemIbsRet.Arredondar(4);
            set => _adRemIbsRet = value.Arredondar(4);
        }

        // UB97
        public decimal vIBSMonoRet
        {
            get => _vIbsMonoRet.Arredondar(2);
            set => _vIbsMonoRet = value.Arredondar(2);
        }

        // UB98
        public decimal adRemCBSRet
        {
            get => _adRemCbsRet.Arredondar(4);
            set => _adRemCbsRet = value.Arredondar(4);
        }

        // UB98a
        public decimal vCBSMonoRet
        {
            get => _vCbsMonoRet.Arredondar(2);
            set => _vCbsMonoRet = value.Arredondar(2);
        }
    }
}