namespace NFe.Classes.Informacoes.Total.IbsCbs.Monofasica
{
    public class gMono
    {
        private decimal _vIBSMono;
        private decimal _vCBSMono;
        private decimal _vIBSMonoReten;
        private decimal _vCBSMonoReten;
        private decimal _vIBSMonoRet;
        private decimal _vCBSMonoRet;

        /// <summary>
        ///     W58 - Total do IBS monofásico
        /// </summary>
        public decimal vIBSMono
        {
            get => _vIBSMono;
            set => _vIBSMono = value.Arredondar(2);
        }

        /// <summary>
        ///     W59 - Total da CBS monofásica
        /// </summary>
        public decimal vCBSMono
        {
            get => _vCBSMono;
            set => _vCBSMono = value.Arredondar(2);
        }

        /// <summary>
        ///     W59a - Total do IBS monofásico sujeito a retenção
        /// </summary>
        public decimal vIBSMonoReten
        {
            get => _vIBSMonoReten;
            set => _vIBSMonoReten = value.Arredondar(2);
        }

        /// <summary>
        ///     W59b - Total da CBS monofásica sujeita a retenção
        /// </summary>
        public decimal vCBSMonoReten
        {
            get => _vCBSMonoReten;
            set => _vCBSMonoReten = value.Arredondar(2);
        }

        /// <summary>
        ///     W59c - Total do IBS monofásico retido anteriormente
        /// </summary>
        public decimal vIBSMonoRet
        {
            get => _vIBSMonoRet;
            set => _vIBSMonoRet = value.Arredondar(2);
        }

        /// <summary>
        ///     W59d - Total da CBS monofásica retida anteriormente
        /// </summary>
        public decimal vCBSMonoRet
        {
            get => _vCBSMonoRet;
            set => _vCBSMonoRet = value.Arredondar(2);
        }
    }
}