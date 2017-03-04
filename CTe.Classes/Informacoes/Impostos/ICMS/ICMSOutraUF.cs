using System;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Impostos.Tipos;
using DFe.Classes;

namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class ICMSOutraUF : ICMSBasico
    {
        private decimal _pRedBcOutraUf;
        private decimal _vBcOutraUf;
        private decimal _pIcmsOutraUf;
        private decimal _vIcmsOutraUf;
        public string CST { get; set; } = "90";

        public decimal pRedBCOutraUF
        {
            get { return _pRedBcOutraUf.Arredondar(2); }
            set { _pRedBcOutraUf = value.Arredondar(2); }
        }

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
