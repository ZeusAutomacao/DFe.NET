using CTe.Classes.Informacoes.Impostos.ICMS.Tipos;
using CTe.Classes.Informacoes.Tipos;
using DFe.Classes;

namespace CTe.Classes.Informacoes.Impostos.ICMS
{
    public class ICMS20 : ICMSBasico
    {
        public ICMS20()
        {
            CST = CST.ICMS20;
        }

        private decimal _pRedBc;
        private decimal _vBc;
        private decimal _pIcms;
        private decimal _vIcms;
        public CST CST { get; set; } 

        public decimal pRedBC
        {
            get { return _pRedBc.Arredondar(2); }
            set { _pRedBc = value.Arredondar(2); }
        }

        public decimal vBC
        {
            get { return _vBc.Arredondar(2); }
            set { _vBc = value.Arredondar(2); }
        }

        public decimal pICMS
        {
            get { return _pIcms.Arredondar(2); }
            set { _pIcms = value.Arredondar(2); }
        }

        public decimal vICMS
        {
            get { return _vIcms.Arredondar(2); }
            set { _vIcms = value.Arredondar(2); }
        }
    }
}