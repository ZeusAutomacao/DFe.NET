namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gMonoReten
    {
        private decimal _qBcMonoReten;
        private decimal _adRemIbsReten;
        private decimal _vIbsMonoReten;
        private decimal _adRemCbsReten;
        private decimal _vCbsMonoReten;

        // UB91
        public decimal qBCMonoReten
        {
            get => _qBcMonoReten.Arredondar(4);
            set => _qBcMonoReten = value.Arredondar(4);
        }

        // UB92
        public decimal adRemIBSReten
        {
            get => _adRemIbsReten.Arredondar(4);
            set => _adRemIbsReten = value.Arredondar(4);
        }

        // UB93
        public decimal vIBSMonoReten
        {
            get => _vIbsMonoReten.Arredondar(2);
            set => _vIbsMonoReten = value.Arredondar(2);
        }

        // UB93a
        public decimal adRemCBSReten
        {
            get => _adRemCbsReten.Arredondar(4);
            set => _adRemCbsReten = value.Arredondar(4);
        }

        // UB93b
        public decimal vCBSMonoReten
        {
            get => _vCbsMonoReten.Arredondar(2);
            set => _vCbsMonoReten = value.Arredondar(2);
        }
    }
}