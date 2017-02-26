using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal.Aereo
{
    public class peri
    {
        public string nONU { get; set; }

        public string qTotEmb { get; set; }

        public infTotAP infTotAP { get; set; }
    }
}