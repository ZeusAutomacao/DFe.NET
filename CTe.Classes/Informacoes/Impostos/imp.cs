using System;
using System.Xml.Serialization;
using DFe.Classes;

namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class imp
    {
        public ICMS ICMS;
        private decimal? _vTotTrib;

        public decimal? vTotTrib
        {
            get { return _vTotTrib.Arredondar(2); }
            set { _vTotTrib = value.Arredondar(2); }
        }

        public bool vTotTribSpecified { get { return vTotTrib.HasValue; } }

        public string infAdFisco { get; set; }

        public ICMSUFFim ICMSUFFim { get; set; }

        public infTribFed infTribFed { get; set; }
    }
}
