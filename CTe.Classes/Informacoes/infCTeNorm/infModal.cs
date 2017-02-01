using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infModal
    {
        private string _versaoModal;

        [XmlAttribute]
        public string versaoModal { get { return _versaoModal; } set { _versaoModal = value; } }

        public rodo rodo { get; set; }

        public rodoOS rodoOS { get; set; }

        public aereo aereo { get; set; }

    }
}
