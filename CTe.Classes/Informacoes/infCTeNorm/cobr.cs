using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class cobr
    {
        public fat fat { get; set; }

        [XmlElement("dup")]
        public List<dup> dup;
    }
}
