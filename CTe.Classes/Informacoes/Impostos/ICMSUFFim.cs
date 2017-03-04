using DFe.Classes;

namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class ICMSUFFim
    {
        private decimal _vBcufFim;
        private decimal _pFcpufFim;
        private decimal _pIcmsufFim;
        private decimal _pIcmsInter;
        private decimal _pIcmsInterPart;
        private decimal _vFcpufFim;
        private decimal _vIcmsufFim;
        private decimal _vIcmsufIni;

        public decimal vBCUFFim
        {
            get { return _vBcufFim.Arredondar(2); }
            set { _vBcufFim = value.Arredondar(2); }
        }

        public decimal pFCPUFFim
        {
            get { return _pFcpufFim.Arredondar(2); }
            set { _pFcpufFim = value.Arredondar(2); }
        }

        public decimal pICMSUFFim
        {
            get { return _pIcmsufFim.Arredondar(2); }
            set { _pIcmsufFim = value.Arredondar(2); }
        }

        public decimal pICMSInter
        {
            get { return _pIcmsInter.Arredondar(2); }
            set { _pIcmsInter = value.Arredondar(2); }
        }

        public decimal pICMSInterPart
        {
            get { return _pIcmsInterPart.Arredondar(2); }
            set { _pIcmsInterPart = value.Arredondar(2); }
        }

        public decimal vFCPUFFim
        {
            get { return _vFcpufFim.Arredondar(2); }
            set { _vFcpufFim = value.Arredondar(2); }
        }

        public decimal vICMSUFFim
        {
            get { return _vIcmsufFim.Arredondar(2); }
            set { _vIcmsufFim = value.Arredondar(2); }
        }

        public decimal vICMSUFIni
        {
            get { return _vIcmsufIni.Arredondar(2); }
            set { _vIcmsufIni = value.Arredondar(2); }
        }
    }
}