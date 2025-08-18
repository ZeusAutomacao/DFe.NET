namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gIBSCBS
    {
        private decimal _vBc;

        // UB16
        public decimal vBC
        {
            get => _vBc.Arredondar(2);
            set => _vBc = value.Arredondar(2);
        }

        // UB17
        public gIBSUF gIBSUF { get; set; }

        // UB36
        public gIBSMun gIBSMun { get; set; }

        // UB55
        public gCBS gCBS { get; set; }

        // UB68
        public gTribRegular gTribRegular { get; set; }

        // UB73
        public gIBSCredPres gIBSCredPres { get; set; }

        // UB78
        public gIBSCredPres gCBSCredPres { get; set; }

        // UB82a
        public gTribCompraGov gTribCompraGov { get; set; }
    }
}