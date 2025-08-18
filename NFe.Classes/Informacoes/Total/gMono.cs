namespace NFe.Classes.Informacoes.Total
{
    public class gMono
    {
        private decimal _vIBSMono;
        private decimal _vCBSMono;
        private decimal _vIBSMonoReten;
        private decimal _vCBSMonoReten;
        private decimal _vIBSMonoRet;
        private decimal _vCBSMonoRet;

        // W58
        public decimal vIBSMono
        {
            get => _vIBSMono;
            set => _vIBSMono = value.Arredondar(2);
        }

        // W59
        public decimal vCBSMono
        {
            get => _vCBSMono;
            set => _vCBSMono = value.Arredondar(2);
        }

        // W59a
        public decimal vIBSMonoReten
        {
            get => _vIBSMonoReten;
            set => _vIBSMonoReten = value.Arredondar(2);
        }

        // W59b
        public decimal vCBSMonoReten
        {
            get => _vCBSMonoReten;
            set => _vCBSMonoReten = value.Arredondar(2);
        }

        // W59c
        public decimal vIBSMonoRet
        {
            get => _vIBSMonoRet;
            set => _vIBSMonoRet = value.Arredondar(2);
        }

        // W59d
        public decimal vCBSMonoRet
        {
            get => _vCBSMonoRet;
            set => _vCBSMonoRet = value.Arredondar(2);
        }
    }
}