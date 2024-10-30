using CTe.Classes.Informacoes.Impostos.ICMS.Tipos;
using CTe.Classes.Informacoes.Tipos;
using DFe.Classes;

namespace CTe.Classes.Informacoes.Impostos.ICMS
{
    public class ICMS60 : ICMSBasico
    {
        public ICMS60()
        {
            CST = CST.ICMS60;
        }

        private decimal _vBcstRet;
        private decimal _vIcmsstRet;
        private decimal _pIcmsstRet;
        private decimal _vCred;
        public CST CST { get; set; }

        public decimal vBCSTRet
        {
            get { return _vBcstRet.Arredondar(2); }
            set { _vBcstRet = value.Arredondar(2); }
        }

        public decimal vICMSSTRet
        {
            get { return _vIcmsstRet.Arredondar(2); }
            set { _vIcmsstRet = value.Arredondar(2); }
        }

        public decimal pICMSSTRet
        {
            get { return _pIcmsstRet.Arredondar(2); }
            set { _pIcmsstRet = value.Arredondar(2); }
        }

        public decimal vCred
        {
            get { return _vCred.Arredondar(2); }
            set { _vCred = value.Arredondar(2); }
        }
    }
}