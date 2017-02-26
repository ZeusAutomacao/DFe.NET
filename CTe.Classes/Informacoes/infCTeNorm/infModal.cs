using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infModal
    {
        [XmlAttribute]
        public versaoModal versaoModal { get; set; }

        public rodo rodo { get; set; }

        public rodoOS rodoOS { get; set; }

        public aereo aereo { get; set; }

    }
}
