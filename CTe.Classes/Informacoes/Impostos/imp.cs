using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class imp
    {
        public ICMS ICMS;

        public decimal? vTotTrib { get; set; }

        public bool vTotTribSpecified { get { return vTotTrib.HasValue; } }

        public string InfAdFisco { get; set; }

        public ICMSUFFim IcmsufFim { get; set; }

        public infTribFed infTribFed { get; set; }
    }
}
