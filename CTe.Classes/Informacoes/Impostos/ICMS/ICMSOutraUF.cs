using CTe.Classes.Informacoes.Impostos.ICMS.Tipos;
using CTe.Classes.Informacoes.Tipos;
using DFe.Classes;

namespace CTe.Classes.Informacoes.Impostos.ICMS
{
    public class ICMSOutraUF : ICMSBasico
    {
        public ICMSOutraUF()
        {
            CST = CST.ICMS90;
        }

        private decimal? _pRedBcOutraUf;
        private decimal _vBcOutraUf;
        private decimal _pIcmsOutraUf;
        private decimal _vIcmsOutraUf;
        public CST CST { get; set; }

        public decimal? pRedBCOutraUF
        {
            get { return _pRedBcOutraUf.Arredondar(2); }
            set { _pRedBcOutraUf = value.Arredondar(2); }
        }

        public bool pRedBCOutraUFSpecified { get { return pRedBCOutraUF.HasValue; } }

        public decimal vBCOutraUF
        {
            get { return _vBcOutraUf.Arredondar(2); }
            set { _vBcOutraUf = value.Arredondar(2); }
        }

        public decimal pICMSOutraUF
        {
            get { return _pIcmsOutraUf.Arredondar(2); }
            set { _pIcmsOutraUf = value.Arredondar(2); }
        }

        public decimal vICMSOutraUF
        {
            get { return _vIcmsOutraUf.Arredondar(2); }
            set { _vIcmsOutraUf = value.Arredondar(2); }
        }
    }
}