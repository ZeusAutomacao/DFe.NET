using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using CTeDLL.Classes.Informacoes.Impostos.Tipos;
using DFe.Classes;

namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class ICMS60 : ICMSBasico
    {
        private decimal _vBcstRet;
        private decimal _vIcmsstRet;
        private decimal _pIcmsstRet;
        private decimal _vCred;
        public CST CST { get; set; } = CST.ICMS60;

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
