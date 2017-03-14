using System;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using CTeDLL.Classes.Informacoes.Impostos.Tipos;
using DFe.Classes;

namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class ICMS20 : ICMSBasico
    {
        private decimal _pRedBc;
        private decimal _vBc;
        private decimal _pIcms;
        private decimal _vIcms;
        public CST CST { get; set; } = CST.ICMS20;

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
