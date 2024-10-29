using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.infCTeNormal.cobrancas;
using System;

namespace CTe.Classes.Informacoes.infCTeNormal
{
    [Serializable]
    public class cobr
    {
        [XmlElement("fat", Order = 1)]
        public fat fat { get; set; }

        [XmlElement("dup", Order = 2)]
        public List<dup> dup;
    }
}