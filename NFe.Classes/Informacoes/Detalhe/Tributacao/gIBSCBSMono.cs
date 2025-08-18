namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gIBSCBSMono
    {
        private decimal _vTotIbsMonoItem;
        private decimal _vTotCbsMonoItem;

        // UB84a
        public gMonoPadrao gMonoPadrao { get; set; }

        // UB90
        public gMonoReten gMonoReten { get; set; }

        // UB94
        public gMonoRet gMonoRet { get; set; }

        // UB99
        public gMonoDif gMonoDif { get; set; }

        // UB104
        public decimal vTotIBSMonoItem
        {
            get => _vTotIbsMonoItem.Arredondar(2);
            set => _vTotIbsMonoItem = value.Arredondar(2);
        }

        // UB105
        public decimal vTotCBSMonoItem
        {
            get => _vTotCbsMonoItem.Arredondar(2);
            set => _vTotCbsMonoItem = value.Arredondar(2);
        }
    }
}